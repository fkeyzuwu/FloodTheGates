using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class CreatureArmy : NetworkBehaviour, ICollectable, IBattlable
{
    public Army army;

    public void Collect(Player player)
    {

    }

    public void StartBattle(IBattlable enemy)
    {
        uint enemyNetId = ((NetworkBehaviour)enemy).GetComponent<NetworkIdentity>().netId;
        CmdCreateBattle(netId, enemyNetId);
    }

    [Command]
    private void CmdCreateBattle(uint netId1, uint netId2)
    {
        BattleSystem.Instance.CreateBattle(netId1, netId2);
    }
}

