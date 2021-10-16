using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.AI;

public class PlayerCombat : NetworkBehaviour
{
    [SerializeField] private Player player;
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

    private Dictionary<KeyCode, CombatCreature> keyCreatureMap = new Dictionary<KeyCode, CombatCreature>();

    private void MapObjectToKey(SyncList<GameObject>.Operation op, int slotIndex, GameObject _, GameObject creatureObj)
    {
        switch (op)
        {
            case SyncList<GameObject>.Operation.OP_ADD:
                CombatCreature creature = new CombatCreature(player, creatureObj, slotIndex);
                keyCreatureMap.Add(selectKeys[slotIndex], creature);
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
                if(keyCreatureMap[keyCode].creature != null)
                {
                    keyCreatureMap[keyCode].creature.Outliner.Activate();
                }
            }

            if (Input.GetKeyUp(keyCode))
            {
                currentlyPressed.Remove(keyCode);
                if (keyCreatureMap[keyCode].creature != null)
                {
                    keyCreatureMap[keyCode].creature.Outliner.Deactivate();
                }
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
        Creature target = GetTargetCreature();

        if (target == null) return;

        foreach (KeyCode keyCode in currentlyPressed)
        {
            if (keyCreatureMap.ContainsKey(keyCode) && keyCreatureMap[keyCode].creatureObj != null)
            {
                if (isSpecialPressed)
                {
                    keyCreatureMap[keyCode].creature.SpecialAttackCreature(target);
                    keyCreatureMap[keyCode].creature.Outliner.Deactivate();
                }
                else
                {
                    keyCreatureMap[keyCode].creature.AttackCreature(target);
                    keyCreatureMap[keyCode].creature.Outliner.Deactivate();
                }
            }
        }

        currentlyPressed.Clear();
    }

    private Creature GetTargetCreature()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            Creature target = hit.transform.GetComponent<Creature>();

            if (target != null && !playerArmy.combatArmy.Contains(target.gameObject))
            {
                return target;
            }
        }

        return null;
    }
}
    
