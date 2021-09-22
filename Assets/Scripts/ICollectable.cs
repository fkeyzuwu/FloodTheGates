using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable : IInteractable
{
    public void Collect(PlayerResources resources);
}
