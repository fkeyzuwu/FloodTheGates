using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;

public class Player : NetworkBehaviour, IBattlable
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerData data;
    [SerializeField] private VisibleEntitiesManager visibleEntitiesManager;

    public VisibleEntitiesManager VisibleEntitiesManager
    {
        get { return visibleEntitiesManager; }
    }

    public PlayerData Data
    {
        get { return data; }
    }

    public PlayerMovement Movement
    {
        get { return movement; }
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
        NetworkBehaviour enemyNb = enemy as NetworkBehaviour;
        uint enemyNetId = enemyNb.GetComponent<NetworkIdentity>().netId;
        CmdCreateBattle(netId, enemyNetId);
    }

    [Command]
    public void CmdCreateBattle(uint netId1, uint netId2)
    {
        BattleManager.CreateBattle(netId1, netId2);
    }
}
