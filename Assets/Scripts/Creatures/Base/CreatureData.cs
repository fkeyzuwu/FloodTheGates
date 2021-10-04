using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Creature")]
public class CreatureData : ScriptableObject
{
    public string Name = "Default";
    public int Amount = 1;

    public int Hp;

    public int Tier;
    public Faction Faction;
}
