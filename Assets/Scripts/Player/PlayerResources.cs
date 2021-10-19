using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerResources : NetworkBehaviour
{
    public SyncDictionary<string, int> Resources = new SyncDictionary<string, int>();
    [SerializeField] private Resource[] resourcePrefabs;
    private ResourceBar resourceUI;

    public override void OnStartServer()
    {
        foreach (Resource resource in resourcePrefabs)
        {
            Resources.Add(resource.Name, 0);
        }
    }

    public override void OnStartLocalPlayer()
    {
        resourceUI = FindObjectOfType<ResourceBar>();
        Resources.Callback += OnResourcesChanged;
    }

    private void OnResourcesChanged(SyncIDictionary<string, int>.Operation op, string key, int item)
    {
        switch (op)
        {
            case SyncIDictionary<string, int>.Operation.OP_SET:
                resourceUI.UpdateResourceUI(key, item);
                break;
            default:
                break;
        }
    }

    [Command]
    public void CmdAddResource(string name, int amount, uint resourceNetId)
    {
        Resources[name] += amount;
        GameObject resource = NetworkServer.spawned[resourceNetId].gameObject;
        EntityManager.Instance.RemoveEntity(resource, resourceNetId);
    }

    [Command]
    private void CmdSubtractResource(string name, int amount)
    {
        Resources[name] -= amount;
        if(Resources[name] < 0)
        {
            Resources[name] = 0;
        }
    }
}
