using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

public class Resource : NetworkBehaviour, ICollectable
{
    [SerializeField] private new string name;
    [SyncVar] public uint parentNetId;

    public override void OnStartClient()
    {
        Transform parent = NetworkClient.spawned[parentNetId].transform;
        transform.SetParent(parent);
    }

    public string Name
    {
        get { return name; }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //maybe doesnt need this, need to decide if want to check from player or from this
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //add cursor change
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //add cursor change
    }
}
