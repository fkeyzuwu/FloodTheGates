using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MapGenerator : NetworkBehaviour
{
    [SerializeField] private Transform resourcesContainer;
    [SerializeField] private GameObject[] resources;

    [SerializeField] private EntityManager entityManager;

    public override void OnStartServer()
    {
        Generate();
    }

    [Server]
    public void Generate()
    {
        foreach(GameObject resource in resources)
        {
            Transform currentContainer = resourcesContainer.Find(resource.GetComponent<Resource>().Name);
            uint containerNetId = currentContainer.GetComponent<NetworkIdentity>().netId;

            for(int i = 0; i < 3; i++)
            {
                GameObject obj = Instantiate(resource, GenerateMapPosition(), GenerateObjectRotation(resource.transform.rotation), currentContainer);
                Resource currentResource = obj.GetComponent<Resource>();
                currentResource.parentNetId = containerNetId;
                currentResource.amount = Random.Range(2, 6) * currentResource.Multiplier;
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

    [Server]
    private Quaternion GenerateObjectRotation(Quaternion baseRotation)
    {
       return Quaternion.Euler(baseRotation.eulerAngles.x, Random.Range(0, 360), baseRotation.eulerAngles.z);
    }
}