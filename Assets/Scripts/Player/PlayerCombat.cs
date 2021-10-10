using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.AI;

public class PlayerCombat : NetworkBehaviour
{
    [SerializeField] private Army playerArmy;
    private new Camera camera;

    private KeyCode attack = KeyCode.Mouse1;
    private KeyCode special = KeyCode.LeftShift;

    private List<KeyCode> currentlyPressed = new List<KeyCode>();
    private bool isSpecialPressed = false;

    void Start()
    {
        camera = Camera.main;
        playerArmy.combatArmy.Callback += MapObjectToKey;
    }

    private List<KeyCode> selectKeys = new List<KeyCode>()
    {
        KeyCode.Q,
        KeyCode.W,
        KeyCode.E,
        KeyCode.R,
        KeyCode.A,
        KeyCode.S,
        KeyCode.D
    };

    private Dictionary<KeyCode, GameObject> keyCreatureMap = new Dictionary<KeyCode, GameObject>();

    private void MapObjectToKey(SyncList<GameObject>.Operation op, int slotIndex, GameObject _, GameObject creature)
    {
        switch (op)
        {
            case SyncList<GameObject>.Operation.OP_ADD:
                keyCreatureMap.Add(selectKeys[slotIndex], creature);
                Debug.Log(selectKeys[slotIndex]);
                break;
            case SyncList<GameObject>.Operation.OP_CLEAR:
                keyCreatureMap.Clear();
                break;
        }
        
    }

    void Update()
    {
        UpdateInputs();

        if (Input.GetKeyDown(attack))
        {
            CallAttack();
        }
    }

    private void UpdateInputs()
    {
        foreach (KeyCode keyCode in selectKeys)
        {
            if (Input.GetKeyDown(keyCode))
            {
                currentlyPressed.Add(keyCode);
            }

            if (Input.GetKeyUp(keyCode))
            {
                currentlyPressed.Remove(keyCode);
            }
        }

        if (Input.GetKeyDown(special))
        {
            isSpecialPressed = true;
        }

        if (Input.GetKeyUp(special))
        {
            isSpecialPressed = false;
        }
    }

    private void CallAttack()
    {
        GameObject target = GetTarget();

        if (target != null && target.GetComponent<Creature>() != null && !playerArmy.combatArmy.Contains(target))
        {
            foreach (KeyCode keyCode in currentlyPressed)
            {
                keyCreatureMap[keyCode].GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
            }
        }
    }

    private GameObject GetTarget()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            return hit.transform.gameObject;
        }

        return null;
    }
}
    
