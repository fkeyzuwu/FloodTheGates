using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

public class Resource : NetworkBehaviour, ICollectable
{
    [SerializeField] private new string name;
    [SyncVar] public int amount;
    [SyncVar] public int minimumAmount;
    [SyncVar] public int maximumAmount;

    public string Name
    {
        get { return name; }
    }

    void OnMouseEnter()
    {
        //change cursor
    }

    void OnMouseExit()
    {
        //change cursor
    }

    public void Collect(Player player)
    {
        player.Resources.CmdAddResource(name, amount, netId);
    }
    
    [Server]
    public void GenerateAmount()
    {
        amount = Random.Range(minimumAmount, maximumAmount);
    }
}
