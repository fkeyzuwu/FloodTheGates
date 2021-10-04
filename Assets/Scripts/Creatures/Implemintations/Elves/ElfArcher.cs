using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfArcher : Creature
{
    public override void Attack()
    {
        Debug.Log("Attacked Very like a  P Ooosy");
    }

    public override void SpecialAttack()
    {
        Debug.Log("Attacked Very Specialy");
    }
}
