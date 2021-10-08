using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class Army : NetworkBehaviour
{
    public SyncList<ArmySlot> slots = new SyncList<ArmySlot>();
    public SyncList<GameObject> combatCreatures = new SyncList<GameObject>();
    public Dictionary<GameObject, CombatCreature> creatureInfo = new Dictionary<GameObject, CombatCreature>();

    public override void OnStartLocalPlayer()
    {
        combatCreatures.Callback += OnSpawnCreature;
    }

    private void OnSpawnCreature(SyncList<GameObject>.Operation op, int itemIndex, GameObject _, GameObject creature)
    {
        if(op == SyncList<GameObject>.Operation.OP_ADD)
        {
            creatureInfo.Add(creature, new CombatCreature(creature, itemIndex));
        }
    }

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
