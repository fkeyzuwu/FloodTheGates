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

    public void OnPointerEnter(PointerEventData eventData)
    {
        //add cursor change
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //add cursor change
    }

    public void Collect(Player player)
    {
        player.Resources.AddResource(name, amount, netId);
    }
    
    [Server]
    public void GenerateAmount()
    {
        amount = Random.Range(minimumAmount, maximumAmount);
    }
}
