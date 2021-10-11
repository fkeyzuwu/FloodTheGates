using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfArcher : Creature
{
    public override void Attack(Creature target)
    {
        base.Attack(target);
        Debug.Log("Attacked Very like a  P Ooosy");
    }

    public override void SpecialAttack(Creature target)
    {
        base.SpecialAttack(target);
        Debug.Log("Attacked Very Specialy");
    }
}
