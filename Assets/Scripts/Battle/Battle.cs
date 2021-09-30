using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class Battle
{
    private List<IBattlable> battlers;
    private List<uint> battlersNetId;

    public Battle(uint netId1, uint netId2)
    {
        battlersNetId.Add(netId1);
        battlersNetId.Add(netId2);
        battlers.Add(NetworkServer.spawned[netId1].GetComponent<IBattlable>());
        battlers.Add(NetworkServer.spawned[netId2].GetComponent<IBattlable>());
        //additive scene bullshit?
        //if only 1 combtant is player - make a scene with one player and 1 ai based on creature
        //if 2 combatants are players - make a scene and add both players to it.

        foreach(IBattlable battler in battlers)
        {
            if (battler is Player)
            {
                Player player = battler as Player;
            }
            else
            {

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
}
