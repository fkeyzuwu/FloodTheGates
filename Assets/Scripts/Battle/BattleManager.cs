using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleManager
{
    public static List<Vector3> battlePositions = new List<Vector3>();
    private static List<Battle> battles = new List<Battle>();
    private static bool initialized = false;

    private static bool isBatteling(uint netId)
    {
        foreach(Battle battle in battles)
        {
            var battlers = battle.GetBattlersByNetId();

            if(battlers.Contains(netId))
            {
                return true;
            }
        }

        return false;
    }

    public static void CreateBattle(uint netId1, uint netId2)
    {
        if (!initialized)
        {
            Initialize();
        }

        if(!isBatteling(netId1) && !isBatteling(netId2))
        {
            battles.Add(new Battle(netId1, netId2)); //starts additive scene bs inside constructor
        }
        else
        {
            Debug.Log("Entites already battling");
        }
    }

    private static void Initialize()
    {
        battlePositions.Add(new Vector3(-2f, 0.5f, 350f));
        battlePositions.Add(new Vector3(2f, 0.5f, 350f));
    }
}
