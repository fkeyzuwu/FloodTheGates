using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Battle
{
    private const string path = "Prefabs/Creatures/";

    private List<IBattlable> battlers = new List<IBattlable>();
    private List<uint> battlersNetId = new List<uint>();

    public Battle(uint netId1, uint netId2)
    {
        battlersNetId.Add(netId1);
        battlersNetId.Add(netId2);
        battlers.Add(NetworkServer.spawned[netId1].GetComponent<IBattlable>());
        battlers.Add(NetworkServer.spawned[netId2].GetComponent<IBattlable>());
        

        int playerCount = 0;
        Scene battleScene = new Scene(); //so we can change it later

        //if only 1 battler is player - make a scene with one player and 1 ai based on creature
        //if 2 battlers are players - 1 player hosts the scene and the other one joins.
        foreach (IBattlable battler in battlers)
        {
            if (battler is Player)
            {
                Player player = battler as Player;
                playerCount++;

                //turn of NavMeshAgent so we can teleport the player
                player.Movement.RpcToggleAgent(false);
                //also enable here the controller for fighting

                if(playerCount == 2)
                {
                    SceneManager.MoveGameObjectToScene(player.gameObject, battleScene);
                    player.transform.position = BattleManager.battlePositions[1];
                }
                else
                {
                    battleScene = ((FTGNetworkManager)NetworkManager.singleton).battleScenes[player.Data.ID];
                    SceneManager.MoveGameObjectToScene(player.gameObject, battleScene);
                    player.transform.position = BattleManager.battlePositions[0];
                } 
            }
            else
            {
                //figure this out later, here are battlers if they arent players
            }
        }

        //do this for each creature in each players army
        GameObject obj = Resources.Load<GameObject>(path + "TestCreature");
        GameObject testCreature = Object.Instantiate(obj, BattleManager.battlePositions[0], Quaternion.identity);
        SceneManager.MoveGameObjectToScene(testCreature, battleScene);

        NetworkServer.Spawn(testCreature);

        Debug.Log($"Battle between {battlers[0]} and {battlers[1]} has started!");
    }

    public List<IBattlable> GetBattlers()
    {
        return battlers;
    }

    public List<uint> GetBattlersByNetId()
    {
        return battlersNetId;
    }

    public void End()
    {
        foreach (IBattlable battler in battlers)
        {
            if(battler is Player)
            {
                Player player = battler as Player;
                SceneManager.MoveGameObjectToScene(player.gameObject, SceneManager.GetSceneAt(0)); //Map scene
                //turn on NavMeshAgent
            }
            else
            {
                NetworkBehaviour nb = battler as NetworkBehaviour;
                NetworkServer.Destroy(nb.gameObject);
            }   
        }
    }
}
