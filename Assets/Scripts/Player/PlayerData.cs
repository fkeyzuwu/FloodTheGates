using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
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
}
