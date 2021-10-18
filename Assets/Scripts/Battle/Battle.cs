using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using Object = UnityEngine.Object;
using System.Threading.Tasks;

public class Battle
{
    private BattleSystem battleSystem;
    private Dictionary<uint, IBattlable> battlers = new Dictionary<uint, IBattlable>();

    private Scene battleScene = new Scene();
    private bool isBattleSceneSet = false;

    public Battle(uint netId1, uint netId2)
    {
        battleSystem = BattleSystem.Instance;
        battlers.Add(netId1, NetworkServer.spawned[netId1].GetComponent<IBattlable>());
        battlers.Add(netId2, NetworkServer.spawned[netId2].GetComponent<IBattlable>());
    }

    public void Start()
    {
        Debug.Log(battleSystem);

        int battlerIndex = 0; //using to get certain positions in the fight

        //if only 1 battler is player - make a scene with one player and 1 ai based on creature
        //if 2 battlers are players - 1 player hosts the scene and the other one joins.
        foreach (IBattlable battler in battlers.Values)
        {
            if (battler is Player)
            {
                SetupPlayerForBattle(battler as Player, battlerIndex);
            }
            else
            {
                SetupAiForBattle(battler, battlerIndex);
            }

            battlerIndex++;
        }

        Debug.Log($"Battle between {GetBattlers()[0]} and {GetBattlers()[1]} has started!");
    }

    private void SetupPlayerForBattle(Player player, int index)
    {
        if (!isBattleSceneSet)
        {
            battleScene = ((FTGNetworkManager)NetworkManager.singleton).battleScenes[player.ID];
            isBattleSceneSet = true;
        }

        player.Combat.currentBattle = this;
        player.positionBeforeBattle = player.transform.position;
        player.rotationBeforeBattle = player.transform.rotation;

        MovePlayerToBattle(player, index);
        SpawnCreaturesForPlayer(player, index);
    }

    private async void SpawnCreaturesForPlayer(Player player, int index)
    {
        float bufferTime = Time.time + 0.15f;
        
        while(Time.time < bufferTime)
        {
            await Task.Yield();
        }

        int creatureIndex = index == 0 ? 0 : 7;

        foreach (ArmySlot slot in player.Army.slots)
        {
            GameObject creature = Resources.Load<GameObject>(battleSystem.creaturePrefabPath + slot.creature);
            Creature creatureScript = creature.GetComponent<Creature>();

            creature.transform.position = battleSystem.creatureStartPositions[creatureIndex].position;
            creature.transform.rotation = battleSystem.armyRotations[index];

            creatureScript.Amount = slot.amount;
            creatureScript.OwnerID = player.ID;

            GameObject creatureObj = Object.Instantiate(creature);
            SceneManager.MoveGameObjectToScene(creatureObj, battleScene);
            player.Army.combatArmy.Add(creatureObj);
            NetworkServer.Spawn(creatureObj, player.connectionToClient);

            creatureIndex++;
        }
    }

    private void SetupAiForBattle(IBattlable battler, int index)
    {
        
    }

    public void End(uint winnerNetId, uint loserNetId)
    {
        IBattlable winner = battlers[winnerNetId];
        IBattlable loser = battlers[loserNetId];

        if(winner is Player)
        {
            Player winnerPlayer = winner as Player;
            winnerPlayer.Army.UpdateArmy();
            DestroyBattleCreatures(winnerPlayer);
            ReturnPlayerToMap(winnerPlayer);
        }
        else
        {
            NetworkServer.Destroy(((NetworkBehaviour)winner).gameObject);
        }

        if(loser is Player)
        {
            Player loserPlayer = loser as Player;
            DestroyBattleCreatures(loserPlayer);
            SendPlayerToMenu(loserPlayer);
        }
        else
        {
            NetworkServer.Destroy(((NetworkBehaviour)loser).gameObject);
        }
    }

    private void DestroyBattleCreatures(Player player)
    {
        foreach(GameObject creatureObj in player.Army.combatArmy)
        {
            NetworkServer.Destroy(creatureObj);
        }

        player.Army.combatArmy.Clear();
    }

    private void MovePlayerToBattle(Player player, int index)
    {
        SceneManager.MoveGameObjectToScene(player.gameObject, battleScene);
        player.Movement.TargetToggleAgent(false);
        player.TargetToggleCombat(true);
        player.TargetSetPositionAndRotation(battleSystem.battlerStartPositions[index].position, battleSystem.armyRotations[index]);
        player.TargetSetCameraMode(CameraControlMode.Battle);
    }

    private void ReturnPlayerToMap(Player player)
    {
        SceneManager.MoveGameObjectToScene(player.gameObject, SceneManager.GetSceneByName("Map"));
        player.TargetToggleCombat(false);
        player.TargetSetPositionAndRotation(player.positionBeforeBattle, player.rotationBeforeBattle);
        player.Movement.TargetToggleAgent(true);
        player.TargetSetCameraMode(CameraControlMode.FollowPlayer); //later change this to whatever 
    }

    private void SendPlayerToMenu(Player player)
    {
        player.TargetDisconnect(); //later take back to offline menu scene or something, or be able to spectate
    }

    #region Getters & Setters

    public List<IBattlable> GetBattlers()
    {
        return battlers.Values.ToList();
    }

    public List<uint> GetBattlersByNetId()
    {
        return battlers.Keys.ToList();
    }

    public Scene GetBattleScene()
    {
        return battleScene;
    }

    #endregion
}
