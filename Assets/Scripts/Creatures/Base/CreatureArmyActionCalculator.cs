using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CreatureArmyActionCalculator
{
    public static CreatureArmyState GetCreatureArmyState(Army playerArmy, Army creatureArmy)
    {
        //calculate here the creaturearmystate based on stats of player army relative to creature army
        return CreatureArmyState.Fight; //for now they always want to fight
    }
}
