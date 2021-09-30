using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;

public class CreatureOnMap : NetworkBehaviour , IBattlable
{
    public InGameCreature Creature { get; set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //add cursor that u can battle dis
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //add cursor that u can battle dis
    }
}
