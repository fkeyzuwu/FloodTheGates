using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SerializeField] private PlayerResources resources;
    [SerializeField] private VisibleEntitiesManager visibleEntitiesManager;
    [SerializeField] private float fogRadius = 5f;

    public VisibleEntitiesManager VisibleEntitiesManager
    {
        get { return visibleEntitiesManager; }
    }

    public float FogRadius
    {
        get { return fogRadius; }
    }
}
