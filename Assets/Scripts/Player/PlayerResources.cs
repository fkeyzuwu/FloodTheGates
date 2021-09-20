using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerResources : NetworkBehaviour
{
    private SyncDictionary<string, int> resources = new SyncDictionary<string, int>();
    [SerializeField] private Resource[] resourcePrefabs;

    void Awake()
    {
        foreach(Resource resource in resourcePrefabs)
        {
            resources.Add(resource.Name, 0);
        }
    }
}
