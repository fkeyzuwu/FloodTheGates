using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerInteractions : NetworkBehaviour
{
    [SerializeField] PlayerResources resources;
    public void Interact(IInteractable interactable)
    {
        if(interactable is ICollectable)
        {
            ICollectable collectable = interactable as ICollectable;
            collectable.Collect(resources);
        }
    }
}
