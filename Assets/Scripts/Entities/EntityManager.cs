using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EntityManager : NetworkBehaviour
{
    private static EntityManager instance = null;

    private SyncHashSet<GameObject> entities = new SyncHashSet<GameObject>();

    void Awake()
    {
        if (!isServer) Destroy(gameObject);

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public static EntityManager Instance //DONT CALL ON CLIENT
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EntityManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "EntityManager";
                    instance = go.AddComponent<EntityManager>();

                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    public void AddEntity(GameObject obj)
    {
        entities.Add(obj);
    }

    public void UpdateEntities()
    {

    }
}
