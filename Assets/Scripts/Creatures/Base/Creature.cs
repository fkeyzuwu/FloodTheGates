using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class Creature : NetworkBehaviour
{
    [SerializeField] private new string name = "Default";
    [SerializeField] private int amount = 1;

    [SerializeField] private int hp;

    [SerializeField] private int tier;
    [SerializeField] private Faction faction;
    
    public abstract void Attack(); //kinda auto attacks
    public abstract void SpecialAttack(); // big bonanza attack dependent on the creature

    #region Getters & Setters

    public string Name
    {
        get { return name; }
    }

    public int Amount 
    { 
        get { return amount; }
        set { amount = value; }
    }

    public int HP
    {
        get { return hp; }
    }

    public int Tier
    {
        get { return tier; }
    }

    public Faction Faction
    {
        get { return faction; }
    }

    #endregion
}
