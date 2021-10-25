using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MapGenerator : NetworkBehaviour
{
    [SerializeField] private Transform resourcesContainer;
    [SerializeField] private GameObject[] resources;
    [SerializeField] private int resourceAmount;

    public override void OnStartServer()
    {
        GenerateMap();
    }

    [Server]
    public void GenerateMap()
    {
        GenerateResources();
        //later add: GenerateTowns, GenerateGates, GenerateArtifacts.. etc
    }

    [Server]
    private void GenerateResources()
    {
        foreach (GameObject resource in resources)
        {
            Transform currentContainer = resourcesContainer.Find(resource.GetComponent<Resource>().Name);

            for (int i = 0; i < resourceAmount; i++)
            {
                GameObject obj = Instantiate(resource, GenerateMapPosition(), GenerateObjectRotation(resource.transform.rotation), currentContainer);
                Resource currentResource = obj.GetComponent<Resource>();
                currentResource.GenerateAmount();
                NetworkServer.Spawn(obj); //figure out a way to set a parent on the client
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