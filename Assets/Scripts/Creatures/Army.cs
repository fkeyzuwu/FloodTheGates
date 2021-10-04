using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Army : NetworkBehaviour
{
    private const string creaturePath = "Prefabs/Creatures/";

    [HideInInspector] public ArmySlot[] slots = new ArmySlot[7];
    private int slotAvailable = 0;

    public void AddCreatureToArmy(Creature creature)
    {
        if (slotAvailable == -1) //no available slot 
        {
            Debug.Log($"No available slots to add creature {creature}");
        } 

        CmdAddCreatureToArmy(creature.Name, creature.Amount);
    }

    [Command]
    private void CmdAddCreatureToArmy(string creatureName, int amount)
    {
        slots[slotAvailable] = new ArmySlot(creatureName, amount);

        if(slotAvailable + 1 == slots.Length) //Out of bounds
        {
            slotAvailable = -1;
        }
    }
}
