using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Battle
{
    private Player player1;
    private uint player1NetId;
    private Player player2;
    private uint player2NetId;

    public Battle(uint netId1, uint netId2)
    {
        player1NetId = netId1;
        player2NetId = netId2;
        player1 = NetworkServer.spawned[netId1].GetComponent<Player>();
        player2 = NetworkServer.spawned[netId2].GetComponent<Player>();
        //additive scene bullshit?

        Debug.Log($"Battle between {player1.gameObject.name} and {player2.gameObject.name} has started!");
    }

    public List<Player> GetPlayers()
    {
        var players = new List<Player>();
        players.Add(player1);
        players.Add(player2);
        return players;
    }

    public List<uint> GetPlayersByNetId()
    {
        var players = new List<uint>();
        players.Add(player1NetId);
        players.Add(player2NetId);
        return players;
    }
}
