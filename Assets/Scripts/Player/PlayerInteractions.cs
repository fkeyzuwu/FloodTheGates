using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerInteractions : NetworkBehaviour
{
    [SerializeField] private Player player;
    public void Interact(IInteractable interactable)
    {
        if(interactable is ICollectable && interactable is IBattlable)
        {
            CreatureArmy creatureArmy = interactable as CreatureArmy;

            //calculate if the army wants to fight/join/leave
            CreatureArmyState cas = CreatureArmyActionCalculator.GetCreatureArmyState(player.Army, creatureArmy.army);

            switch (cas)
            {
                case CreatureArmyState.Fight:
                    player.StartBattle(creatureArmy);
                    break;
                case CreatureArmyState.Run:
                    //show ui for letting you decide if to let them run or not
                    break;
                case CreatureArmyState.Collect:
                    //show ui for letting you decide if you want to collect or not
                    break;
            }

            return;
        }

        if(interactable is ICollectable collectable)
        {
            collectable.Collect(player);
            return;
        }

        if(interactable is IBattlable enemy)
        {
            if((Object)enemy != player)
            {
                player.StartBattle(enemy);
            }
            return;
        }
    }
}
