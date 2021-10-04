using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature
{
    public string Name { get; }
    public int Amount { get; set; } = 1;
    public int Tier { get; }
    public Faction Faction { get; }
    public int Hp { get; set; }
    public abstract void Attack(); //kinda auto attacks
    public abstract void SpecialAttack(); // big bonanza attack dependent on the creature
}
