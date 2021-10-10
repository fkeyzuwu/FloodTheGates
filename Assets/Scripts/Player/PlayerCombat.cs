using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PlayerCombat : NetworkBehaviour
{
    [SerializeField] private Army playerArmy;
    void Start()
    {
        playerArmy.combatArmy.Callback += MapObjectToKey;
    }

    private List<KeyCode> keyCodes = new List<KeyCode>()
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
                keyCreatureMap.Add(keyCodes[slotIndex], creature);
                Debug.Log(keyCodes[slotIndex]);
                break;
            case SyncList<GameObject>.Operation.OP_CLEAR:
                keyCreatureMap.Clear();
                break;
        }
        
    }
}
    
