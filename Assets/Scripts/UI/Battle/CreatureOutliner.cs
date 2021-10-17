using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureOutliner : MonoBehaviour
{
    [SerializeField] private Outline outline;

    public void Activate()
    {
        outline.OutlineWidth = 3f;
    }

    public void Deactivate()
    {
        outline.OutlineWidth = 0f;
    }
}
