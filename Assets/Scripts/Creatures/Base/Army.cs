using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Army : NetworkBehaviour
{
    public SyncList<ArmySlot> slots = new SyncList<ArmySlot>();
    public SyncList<GameObject> combatArmy = new SyncList<GameObject>();

    public void AddCreatureToArmy(Creature creature)
    {
        CmdAddCreatureToArmy(creature.Data.Name, creature.Data.Amount);
    }

    [Command]
    private void CmdAddCreatureToArmy(string creatureName, int amount)
    {
        if(slots.Count == 7) //Out of bounds
        {
            Debug.Log("Max Army");
            return;
        }

        slots.Add(new ArmySlot(creatureName, amount));
        Debug.Log($"{slots[slots.Count - 1].creature}, {slots[slots.Count - 1].amount}");
    }
}
