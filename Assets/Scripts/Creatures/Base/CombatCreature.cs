using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatCreature
{
    public Player owner;
    public GameObject creatureObj;
    public Creature creature;
    public NavMeshAgent agent;
    public int slotIndex;

    public CombatCreature(Player owner, GameObject creatureObj, int slotIndex)
    {
        this.owner = owner;
        this.creatureObj = creatureObj;
        this.slotIndex = slotIndex;

        agent = creatureObj.GetComponent<NavMeshAgent>();
        creature = creatureObj.GetComponent<Creature>();
    }
}
