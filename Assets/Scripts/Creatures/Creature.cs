using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    public abstract string Name { get; }
    public abstract string Amount { get; }
    public abstract int Tier { get; }
    public abstract Faction Faction { get; }
    public abstract int Hp { get; set; }
    //whatever add other stats later
    public abstract void Attack(); //kinda auto attacks
    public abstract void SpecialAttack(); // big bonanza attack dependent on the creature

}
