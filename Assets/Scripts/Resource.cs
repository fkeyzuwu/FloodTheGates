using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Resource : MonoBehaviour, ICollectable
{
    [SerializeField] private new string name;

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
