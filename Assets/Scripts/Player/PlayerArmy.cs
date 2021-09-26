using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerArmy : NetworkBehaviour
{
    private Army localArmy;
    public SyncDictionary<string, int> Army = new SyncDictionary<string, int>();
}
