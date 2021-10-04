using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;
using System;

public class Player : NetworkBehaviour, IBattlable
{
    [SyncVar] private int id;

    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private PlayerResources resources;
    [SerializeField] private Army army;

    [SerializeField] private PlayerMovement movement;

    #region Getters & Setters

    public PlayerInventory Inventory
    {
        get { return inventory; }
    }

    public PlayerResources Resources
    {
        get { return resources; }
    }

    public Army Army
    {
        get { return army; }
    }
    
    public PlayerMovement Movement
    {
        get { return movement; }
    }

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    #endregion

    public void OnPointerEnter(PointerEventData eventData)
    {
        //add cursor change
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //add cursor change
    }

    public void StartBattle(IBattlable enemy)
    {
        NetworkBehaviour enemyNb = enemy as NetworkBehaviour;
        uint enemyNetId = enemyNb.GetComponent<NetworkIdentity>().netId;
        CmdCreateBattle(netId, enemyNetId);
    }

    [Command]
    public void CmdCreateBattle(uint netId1, uint netId2)
    {
        BattleSystem.Instance.CreateBattle(netId1, netId2);
    }

    [ClientRpc]
    public void RpcSetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
