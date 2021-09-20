using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class VisibleEntitiesManager : NetworkBehaviour
{
    private SyncHashSet<GameObject> visibleEntities = new SyncHashSet<GameObject>();

    private void RequestEntityUpdate()
    {
        CmdRequestEntityUpdate();
    }

    [Command]
    private void CmdRequestEntityUpdate(NetworkConnectionToClient sender = null)
    {
        EntityManager.Instance.UpdateEntities(sender);
    }

    [TargetRpc]
    public void TargetSpawnEntities(List<GameObject> entities)
    {
        foreach (GameObject entity in entities)
        {
            GameObject obj = Instantiate(entity);
        }
    }
}
