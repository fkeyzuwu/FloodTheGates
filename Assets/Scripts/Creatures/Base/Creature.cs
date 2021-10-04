using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;

public abstract class Creature : NetworkBehaviour, ICollectable
{
    [SerializeField] private CreatureData data;
    private int armySlotIndex = -1;
    public abstract void Attack(); //kinda auto attacks
    public abstract void SpecialAttack(); // big bonanza attack dependent on the creature

    public void Collect(Player player)
    {
        player.Army.AddCreatureToArmy(this);
        Debug.Log("Added");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public CreatureData Data
    {
        get { return data; }
    }

    public int ArmySlotIndex
    {
        get { return armySlotIndex; }
    }
}
