using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class VisibleEntitiesManager : NetworkBehaviour
{
    private SyncHashSet<GameObject> visibleEntities = new SyncHashSet<GameObject>();

    public void AddEntity()
    {

    }
}
