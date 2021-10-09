using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PlayerCombat : NetworkBehaviour
{
    private new Camera camera;

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

    private List<KeyCode> currentlyPressed = new List<KeyCode>();
    void Awake()
    {
        camera = Camera.main;
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

        if (currentlyPressed.Contains(inputs["SpecialAttack"]))
        {
            CallSpecialAttack();
        }

        return;
    }

    private void CallAttack()
    {
        //call all units pressed on qwerasd to attack where you pressed

        currentlyPressed.Clear();
    }

    private void CallSpecialAttack()
    {
        //call all units pressed on qwerasd to attack where you pressed

        currentlyPressed.Clear();
    }
}
