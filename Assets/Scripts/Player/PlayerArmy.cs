using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerArmy : NetworkBehaviour
{
    private const string creaturePath = "Prefabs/Creatures/";

    private Army localArmy; //don't use on server
    public SyncDictionary<string, int> Army = new SyncDictionary<string, int>();

    public void AddCreatureToArmy(InGameCreature inGameCreature, int amount)
    {
        CmdAddCreatureToArmy(inGameCreature.creature.Name, inGameCreature.amount);
    }
    
    [Command]
    public void CmdAddCreatureToArmy(string creatureName, int amount)
    {
        if(Army.Count < 7)
        {
            Army.Add(creatureName, amount);
        }
        else
        {
            Debug.Log($"{creatureName} cannot be added since army count is already at max.");
        }
    }
}
