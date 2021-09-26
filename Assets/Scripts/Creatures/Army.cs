using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    [HideInInspector] public Creature[] Creatures { get; set; } = new Creature[7];
}
