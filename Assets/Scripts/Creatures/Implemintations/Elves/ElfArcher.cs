using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfArcher : Creature
{
    public override void AttackCreature(Creature target)
    {
        base.AttackCreature(target);
        Debug.Log("Attacked Very like a  P Ooosy");
    }

    public override void SpecialAttackCreature(Creature target)
    {
        base.SpecialAttackCreature(target);
        Debug.Log("Attacked Very Specialy");
    }
}
