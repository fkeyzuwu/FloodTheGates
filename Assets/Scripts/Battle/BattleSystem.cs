using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.SceneManagement;

public class BattleSystem : NetworkBehaviour
{
    #region Singleton

    private static BattleSystem instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static BattleSystem Instance //DONT CALL ON CLIENT
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BattleSystem>();

                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "BattleSystem";
                    instance = go.AddComponent<BattleSystem>();

                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    #endregion

    [HideInInspector] public string creaturePrefabPath = "Prefabs/Creatures/";
    public Transform[] battlerStartPositions;
    public Transform[] creatureStartPositions;
    [HideInInspector] public Quaternion[] armyRotations = new Quaternion[2];

    private List<Battle> battles = new List<Battle>();

    public override void OnStartServer()
    {
        armyRotations[0] = Quaternion.Euler(new Vector3(0, 0, 0));
        armyRotations[1] = Quaternion.Euler(new Vector3(0, 180, 0));
    }

    [Server]
    private bool isBatteling(uint netId)
    {
        foreach (Battle battle in battles)
        {
            var battlers = battle.GetBattlersByNetId();

            if (battlers.Contains(netId))
            {
                return true;
            }
        }

        return false;
    }

    [Server]
    public void CreateBattle(uint netId1, uint netId2)
    {
        if (!isBatteling(netId1) && !isBatteling(netId2))
        {
            Battle battle = new Battle(netId1, netId2);
            battles.Add(battle);
            battle.Start();
        }
        else
        {
            Debug.Log("Entites already battling");
        }
    }

    [Server]
    public void EndBattle(Battle battle, uint winnerNetId, uint loserNetId)
    {
        if(!battles.Contains(battle))
        { 
            Debug.Log("battle fucked"); 
            return; 
        }

        battle.End(winnerNetId, loserNetId);
        battles.Remove(battle);

        if(NetworkManager.singleton.numPlayers == 1)
        {
            Debug.Log(NetworkServer.spawned[winnerNetId].gameObject + "won the game!");
        }
    }
}
