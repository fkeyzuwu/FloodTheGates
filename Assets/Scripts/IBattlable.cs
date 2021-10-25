using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattlable : IInteractable
{
    public void StartBattle(IBattlable enemy);
}
