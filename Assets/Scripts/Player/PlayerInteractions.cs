using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerInteractions : NetworkBehaviour
{
    [SerializeField] private Player player;
    public void Interact(IInteractable interactable)
    {
        if(interactable is ICollectable)
        {
            ICollectable collectable = interactable as ICollectable;
            collectable.Collect(player.Data);
            return;
        }

        if(interactable is IBattlable)
        {
            IBattlable enemy = interactable as IBattlable;
            player.StartBattle(enemy);
            return;
        }
    }
}
