using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleManager
{
    private static List<Battle> battles = new List<Battle>();

    private static bool isBatteling(uint netId1, uint netId2)
    {
        foreach(Battle battle in battles)
        {
            var players = battle.GetPlayersByNetId();

            if(players.Contains(netId1) && players.Contains(netId2))
            {
                return true;
            }
        }

        return false;
    }

    public static void CreateBattle(uint netId1, uint netId2)
    {
        if(!isBatteling(netId1, netId2))
        {
            battles.Add(new Battle(netId1, netId2)); //starts additive scene bs inside constructor
        }
        else
        {
            Debug.Log("Players already started a fight");
        }
    }
}
