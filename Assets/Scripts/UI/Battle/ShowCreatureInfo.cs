using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCreatureInfo : MonoBehaviour
{
    [SerializeField] private CreatureBattleUI ui;

    private bool isMouseOnCreature = false;
    private float timeTillToggle = 0.3f;

    public void OnMouseEnter()
    {
        isMouseOnCreature = true;
        StartCoroutine(WaitToToToggleStats());
    }

    public void OnMouseExit()
    {
        isMouseOnCreature = false;
        ui.ToggleStatsWindow(false);
        //change cursor
    }

    IEnumerator WaitToToToggleStats()
    {
        yield return new WaitForSeconds(timeTillToggle);

        if (isMouseOnCreature)
        {
            ui.ToggleStatsWindow(true);
            //change cursor
        }
    }
}
