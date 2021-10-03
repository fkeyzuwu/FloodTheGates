using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCombat : NetworkBehaviour
{
    [SerializeField] private KeyCode specialAttack;

    void Awake()
    {
        enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {

        }
    }
}
