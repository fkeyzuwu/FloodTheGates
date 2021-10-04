using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class Creature : NetworkBehaviour
{
    [SerializeField] private CreatureData data;
    private int armySlotIndex = -1;
    public abstract void Attack(); //kinda auto attacks
    public abstract void SpecialAttack(); // big bonanza attack dependent on the creature

    public CreatureData Data
    {
        get { return data; }
    }

    public int ArmySlotIndex
    {
        get { return armySlotIndex; }
    }
}
