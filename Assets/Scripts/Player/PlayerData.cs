using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerData : NetworkBehaviour
{
    [SyncVar] private int id;
    [SerializeField] private PlayerResources resources;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private PlayerArmy army;

    public PlayerResources Resources
    {
        get { return resources; }
    }

    public PlayerArmy Army
    {
        get { return army; }
    }

    public int ID
    {
        get { return id; }
        set { id = value; }
    }
}
