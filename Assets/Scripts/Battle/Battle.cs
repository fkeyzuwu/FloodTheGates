using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Battle
{
    private IBattlable combatant1;
    private uint combatant1NetId;
    private IBattlable combatant2;
    private uint combatant2NetId;

    public Battle(uint netId1, uint netId2)
    {
        combatant1NetId = netId1;
        combatant2NetId = netId2;
        combatant1 = NetworkServer.spawned[netId1].GetComponent<IBattlable>();
        combatant2 = NetworkServer.spawned[netId2].GetComponent<IBattlable>();
        //additive scene bullshit?
        //if only 1 combtant is player - make a scene with one player and 1 ai based on creature
        //if 2 combatants are players - make a scene and add both players to it.

        Debug.Log($"Battle between {combatant1} and {combatant2} has started!");
    }

    public List<IBattlable> GetCombatants()
    {
        var combatants = new List<IBattlable>();
        combatants.Add(combatant1);
        combatants.Add(combatant2);
        return combatants;
    }

    public List<uint> GetCombatantsByNetId()
    {
        var combatants = new List<uint>();
        combatants.Add(combatant1NetId);
        combatants.Add(combatant2NetId);
        return combatants;
    }
}
