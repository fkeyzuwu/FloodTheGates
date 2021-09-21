using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MapGenerator : NetworkBehaviour
{
    [SerializeField] private Transform resourcesContainer;
    [SerializeField] private GameObject[] resources;

    [SerializeField] private EntityManager entityManager;

    void Update()
    {
        if (!isServer) return;

        if (Input.GetKeyDown(KeyCode.K))
        {
            Generate();
        }
    }

    [Server]
    public void Generate()
    {
        foreach(GameObject resource in resources)
        {
            Transform currentContainer = resourcesContainer.Find(resource.GetComponent<Resource>().Name);
            uint containerNetId = currentContainer.GetComponent<NetworkIdentity>().netId;

            for(int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(resource, GenerateMapPosition(), Quaternion.identity, currentContainer);
                obj.GetComponent<Resource>().parentNetId = containerNetId;
                entityManager.AddEntity(obj ,obj.GetComponent<NetworkIdentity>().netId);
            }
        }
    }

    [Server]
    private Vector3 GenerateMapPosition()
    {
        float x = Random.Range(-50, 50);
        float z = Random.Range(-50, 50);

        return new Vector3(x, 0, z);
    }
}