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
        CmdAddCreatureToArmy(creature.Data.Name, creature.Amount);
    }

    [Command]
    private void CmdAddCreatureToArmy(string creatureName, int amount)
    {
        Debug.Log(creatureName);

        if (slots.Count == 7) //Out of bounds
        {
            Debug.Log("Max Army");
            return;
        }

        slots.Add(new ArmySlot(creatureName, amount));
        Debug.Log($"{slots[slots.Count - 1].creature}, {slots[slots.Count - 1].amount}");
    }

    [Server]
    private void UpdateArmy() //call this at the end of battle
    {
        slots.Clear();

        for(int i = 0; i < combatArmy.Count; i++)
        {
            Creature creature = combatArmy[i].GetComponent<Creature>();
            slots.Add(new ArmySlot(creature.Data.Name, creature.Amount));
        }
    }
}
