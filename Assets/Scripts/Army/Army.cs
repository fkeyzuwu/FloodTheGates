using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class Army : NetworkBehaviour
{
    public SyncList<ArmySlot> slots = new SyncList<ArmySlot>();
    public SyncList<GameObject> combatArmy = new SyncList<GameObject>();

    public void AddCreatureToArmy(Creature creature)
    {
        CmdAddCreatureToArmy(creature.Data.Name, creature.Amount, creature.netId);
    }

    [Command]
    private void CmdAddCreatureToArmy(string creatureName, int amount, uint creatureNetId)
    {
        if (slots.Count == 7) //Out of bounds
        {
            return;
        }

        slots.Add(new ArmySlot(creatureName, amount));
        GameObject creatureObj = NetworkServer.spawned[creatureNetId].gameObject;
        NetworkServer.Destroy(gameObject);
    }

    [Server]
    public void UpdateArmy() //call this at the end of battle
    {
        slots.Clear();

        for(int i = 0; i < combatArmy.Count; i++)
        {
            Creature creature = combatArmy[i].GetComponent<Creature>();
            slots.Add(new ArmySlot(creature.Data.Name, creature.Amount));
        }
    }
}
