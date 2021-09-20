using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Transform resourcesContainer;
    [SerializeField] private GameObject[] resources;

    [SerializeField] private EntityManager entityManager;

    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        foreach(GameObject resource in resources)
        {
            Transform currentContainer = resourcesContainer.Find(resource.GetComponent<Resource>().Name);

            for(int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(resource, GenerateMapPosition(), Quaternion.identity , currentContainer);
                entityManager.AddEntity(obj);
            }
        }
    }

    private Vector3 GenerateMapPosition()
    {
        float x = Random.Range(-50, 50);
        float z = Random.Range(-50, 50);

        return new Vector3(x, 0, z);
    }
}
