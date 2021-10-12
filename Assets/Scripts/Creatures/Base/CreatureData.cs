using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Creature")]
public class CreatureData : ScriptableObject
{
    [Header("Name")]
    public string Name = "Default";

    [Header("Stats")]
    public int HpPerUnit = 10;
    public int Attack = 1;
    public float AttackRange = 3.0f;
    public float AttackSpeed = 1.0f;

    [Header("Info")]
    public int Tier = 1;
    public Faction Faction;

  
}
