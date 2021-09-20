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
        if (!isServer) return;

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
        TempUpdateEntities(obj);
    }

    [Server]
    public void UpdateEntities(NetworkConnectionToClient sender)
    {
        Player player = sender.identity.GetComponent<Player>();
        Vector3 senderPosition = sender.identity.transform.position;
        var colliders = Physics.OverlapSphere(senderPosition, player.FogRadius);

        List<GameObject> entities = new List<GameObject>();

        foreach(Collider collider in colliders)
        {
            entities.Add(collider.gameObject);
        }

        player.VisibleEntitiesManager.TargetSpawnEntities(entities);
    }

    public void TempUpdateEntities(GameObject entity)
    {
        NetworkServer.Spawn(entity);
    }

    
}
