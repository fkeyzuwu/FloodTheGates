using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerResources : NetworkBehaviour
{
    public SyncDictionary<string, int> Resources = new SyncDictionary<string, int>();
    [SerializeField] private Resource[] resourcePrefabs;
    private ResourceBar resourceUI;

    void Awake()
    {
        foreach(Resource resource in resourcePrefabs)
        {
            Resources.Add(resource.Name, 0);
        }
    }

    public override void OnStartLocalPlayer()
    {
        resourceUI = FindObjectOfType<ResourceBar>();
    }

    public void AddResource(string name, int amount, uint resourceNetId)
    {
        CmdAddResource(name, amount, resourceNetId);
        resourceUI.UpdateResourceUI(name ,Resources[name]);
    }

    [Command]
    private void CmdAddResource(string name, int amount, uint resourceNetId)
    {
        Resources[name] += amount;
        GameObject resource = NetworkServer.spawned[resourceNetId].gameObject;
        EntityManager.Instance.RemoveEntity(resource, resourceNetId);
    }

    public void SubtractResource(string name, int amount)
    {
        CmdSubtractResource(name, amount);
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
