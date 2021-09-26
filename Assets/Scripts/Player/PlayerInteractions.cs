using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerInteractions : NetworkBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerResources resources;
    public void Interact(IInteractable interactable)
    {
        if(interactable is ICollectable)
        {
            ICollectable collectable = interactable as ICollectable;
            collectable.Collect(resources);
            return;
        }

        if(interactable is Player)
        {
            //begin battle
            Player enemy = interactable as Player;
            player.StartBattle(enemy);
            return;
        }
    }
}
