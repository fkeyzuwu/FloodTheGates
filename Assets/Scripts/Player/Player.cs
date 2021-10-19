using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;
using System;

public class Player : NetworkBehaviour, IBattlable
{
    [SyncVar] private int id;
    [SyncVar] public Vector3 positionBeforeBattle;
    [SyncVar] public Quaternion rotationBeforeBattle;

    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private PlayerResources resources;
    [SerializeField] private Army army;
    [SerializeField] private PlayerCombat combat;

    [SerializeField] private PlayerMovement movement;

    private new Camera camera;
    private CameraMovement camScript;

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

    public PlayerCombat Combat
    {
        get { return combat; }
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

    public override void OnStartLocalPlayer()
    {
        camera = Camera.main;
        camScript = camera.GetComponent<CameraMovement>();
        camScript.InitializeCamera(transform);
    }

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
        uint enemyNetId = ((NetworkBehaviour)enemy).GetComponent<NetworkIdentity>().netId;
        CmdCreateBattle(netId, enemyNetId);
    }

    [Command]
    public void CmdCreateBattle(uint netId1, uint netId2)
    {
        BattleSystem.Instance.CreateBattle(netId1, netId2);
    }

    [TargetRpc]
    public void TargetSetPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
    }

    [TargetRpc]
    public void TargetSetCameraMode(CameraControlMode controlMode) //change this so this works with any mode
    {
        camScript.SetupCamera(controlMode);
    }

    [TargetRpc]
    public void TargetToggleCombat(bool toggle)
    {
        combat.enabled = toggle;
    }
}
