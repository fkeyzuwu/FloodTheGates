using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;
using System;

public class PlayerCombat : NetworkBehaviour
{
    #region Input Mappings

    private Dictionary<string, KeyCode> inputs = new Dictionary<string, KeyCode>()
    {
        { "Attack", KeyCode.Mouse1 },
        { "Special", KeyCode.LeftShift },
        { "ArmySlot0", KeyCode.Q },
        { "ArmySlot1", KeyCode.W },
        { "ArmySlot2", KeyCode.E },
        { "ArmySlot3", KeyCode.R },
        { "ArmySlot4", KeyCode.A },
        { "ArmySlot5", KeyCode.S },
        { "ArmySlot6", KeyCode.D },
    };

    #endregion

    private new Camera camera;

    private Army playerArmy;

    private List<KeyCode> currentlyPressed = new List<KeyCode>();
    void Awake()
    {
        camera = Camera.main;
        playerArmy = GetComponent<Army>();
        enabled = false;
    }

    void Update()
    {
        UpdateInputs();
        CheckForAction();
    }

    private void UpdateInputs()
    {
        foreach(KeyValuePair<string, KeyCode> input in inputs)
        {
            if (Input.GetKeyDown(input.Value))
            {
                currentlyPressed.Add(input.Value);
            }

            if (Input.GetKeyUp(input.Value))
            {
                currentlyPressed.Remove(input.Value);
            }
        }
    }

    private void CheckForAction()
    {
        if (currentlyPressed.Count == 0) return;

        if (currentlyPressed.Contains(inputs["Attack"]))
        {
            CallAttack();
        }
    }

    private void CallAttack()
    {
        bool isSpecial = false;
        Creature targetCreature = GetTargetCreature();

        if (targetCreature != null && !playerArmy.combatCreatures.Contains(targetCreature.gameObject))
        {
            currentlyPressed.Remove(inputs["Attack"]);

            if (currentlyPressed.Remove(inputs["SpecialAttack"]))
            {
                isSpecial = true;
            }

            foreach(KeyCode input in currentlyPressed)
            {
                
            }
            //call all units pressed on qwerasd to attack where you pressed
        }

        currentlyPressed.Clear();
    }

    private Creature GetTargetCreature()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            Creature creature = hit.transform.GetComponent<Creature>();

            if (creature != null) //Add check to see if isnt a creature in our army
            {
                return creature;
            }
        }

        return null;
    }
}
