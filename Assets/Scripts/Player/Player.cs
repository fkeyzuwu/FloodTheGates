using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SerializeField] private PlayerResources resources;
    [SerializeField] private VisibleEntitiesManager visibleEntitiesManager;

    public VisibleEntitiesManager VisibleEntitiesManager
    {
        get { return visibleEntitiesManager; }
    }

    public PlayerResources Resources
    {
        get { return resources; }
    }
}
