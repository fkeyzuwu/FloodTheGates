using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class Battle
{
    private Scene battleScene = new Scene();
    private List<IBattlable> battlers = new List<IBattlable>();
    private List<uint> battlersNetId = new List<uint>();
    public bool Initialized { get; } = false;

    public Battle(uint netId1, uint netId2)
    {
        battlersNetId.Add(netId1);
        battlersNetId.Add(netId2);
        battlers.Add(NetworkServer.spawned[netId1].GetComponent<IBattlable>());
        battlers.Add(NetworkServer.spawned[netId2].GetComponent<IBattlable>());

        bool isBattleSceneSet = false;

        //if only 1 battler is player - make a scene with one player and 1 ai based on creature
        //if 2 battlers are players - 1 player hosts the scene and the other one joins.
        foreach (IBattlable battler in battlers)
        {
            if (battler is Player)
            {
                Player player = battler as Player;

                if(!isBattleSceneSet)
                {
                    battleScene = ((FTGNetworkManager)NetworkManager.singleton).battleScenes[player.ID];
                    isBattleSceneSet = true;
                }

                SceneManager.MoveGameObjectToScene(player.gameObject, battleScene);
                player.Movement.RpcToggleAgent(false);
                player.RpcToggleCombat(true);
            }
            else
            {
                //figure this out later, here are battlers if they arent players
            }
        }

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

    public Scene GetBattleScene()
    {
        return battleScene;
    }

    public void End()
    {
        foreach (IBattlable battler in battlers)
        {
            if(battler is Player)
            {
                Player player = battler as Player;
                SceneManager.MoveGameObjectToScene(player.gameObject, SceneManager.GetSceneByName("Map"));
                //turn on NavMeshAgent
            }
            else
            {
                NetworkServer.Destroy(((NetworkBehaviour)battler).gameObject);
            }   
        }
    }
}
