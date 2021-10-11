using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Creature")]
public class CreatureData : ScriptableObject
{
    public string Name = "Default";
    public int Amount = 1;

    public int Hp = 10;
    public int Attack = 1;
    public float AttackRange = 3.0f;
    public float AttackSpeed = 1.0f;

    public int Tier;
    public Faction Faction;
}
