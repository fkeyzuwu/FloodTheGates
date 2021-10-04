using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

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

    private const string path = "Prefabs/Creatures/";

    [SerializeField] private Transform[] battlerStartPositions;
    [SerializeField] private Transform[] creatureStartPositions;
    [SerializeField] private Transform cameraPosition;

    private List<Battle> battles = new List<Battle>();

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

    public void CreateBattle(uint netId1, uint netId2)
    {
        if (!isBatteling(netId1) && !isBatteling(netId2))
        {
            Battle newBattle = new Battle(netId1, netId2);
            battles.Add(newBattle);
            InitializeBattle(newBattle);
        }
        else
        {
            Debug.Log("Entites already battling");
        }
    }

    public void InitializeBattle(Battle battle)
    {
        StartCoroutine(InitializePositions(battle));
    }

    IEnumerator InitializePositions(Battle battle)
    {
        yield return new WaitForSeconds(0.05f); //hopefully enough so players get the navmesh thingy

        InitializePlayerPositions(battle);
    }

    private void InitializePlayerPositions(Battle battle)
    {
        int i = 0;
        int playerCount = 0;

        foreach (IBattlable battler in battle.GetBattlers())
        {
            if(battler is Player)
            {
                Player player = battler as Player;
                player.RpcSetPosition(battlerStartPositions[i].position);
                player.RpcSetRotation(Quaternion.Euler(new Vector3(0, 0, 0)));
                player.RpcSetCameraPosition(cameraPosition.position);
                player.RpcSetCameraRotation(Quaternion.Euler(new Vector3(0,0,0)));

                SpawnCreatures(battle);
            }
            else
            {
                //whatever incase they are ai niggas
            }

            i++;
        }
    }

    private void SpawnCreatures(Battle battle)
    {
        foreach(IBattlable battlable in battle.GetBattlers())
        {
            if(battlable is Player)
            {
                Player player = battlable as Player;
                
                foreach(ArmySlot slot in player.Army.slots)
                {
                    GameObject creature = Resources.Load<GameObject>(path + slot.creature);
                    creature.GetComponent<Creature>().Data.Amount = slot.amount;
                    Instantiate(creature);
                    //make their positions and rotations based on whos players who
                    NetworkServer.Spawn(creature, player.connectionToClient);
                }
            }
        }
    }
}
