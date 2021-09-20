using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EntityManager : NetworkBehaviour
{
    private SyncHashSet<GameObject> entities = new SyncHashSet<GameObject>();

    public void AddEntity(GameObject obj)
    {
        entities.Add(obj);
    }
}
