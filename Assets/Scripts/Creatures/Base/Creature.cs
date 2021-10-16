using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using System;

public abstract class Creature : NetworkBehaviour, ICollectable
{
    private static string creatureDataPath = "ScriptableObjects\\Creatures\\";
    public int Health => (HealthPerUnit * Amount) - (HealthPerUnit - CurrentUnitHealth);
    public int Attack => AttackPerUnit * Amount;
    public int HealthPerUnit => data.HealthPerUnit;
    public int AttackPerUnit => data.AttackPerUnit;
    [SyncVar] public int CurrentUnitHealth;

    [SyncVar] public int Amount = 1; //default

    [SyncVar] public int OwnerID = -1;
    [SyncVar] private CreatureData data;
    private NavMeshAgent agent;
    //private Animator animator; //Get Animator later
    private CreatureBattleUI ui;
    private CreatureOutliner outliner;
    [SyncVar] private int armySlotIndex = -1;

    void Start()
    {
        string prefabName = name.Replace("(Clone)", "");
        data = Resources.Load<CreatureData>(creatureDataPath + prefabName);
        agent = GetComponent<NavMeshAgent>();
        outliner = GetComponent<CreatureOutliner>();
        ui = GetComponentInChildren<CreatureBattleUI>();

        CurrentUnitHealth = HealthPerUnit;
    }

    public virtual void AttackCreature(Creature target) //kinda auto attacks
    {
        StopAllCoroutines();
        StartCoroutine(MoveToTarget(target));
    }
    public virtual void SpecialAttackCreature(Creature target) // big bonanza attack dependent on the creature
    {
        StopAllCoroutines();
        StartCoroutine(MoveToTarget(target));
    }

    IEnumerator MoveToTarget(Creature target)
    {
        GameObject targetObj = target.gameObject;

        while (targetObj != null && data.AttackRange <= Vector3.Distance(transform.position, target.transform.position))
        {
            agent.SetDestination(target.transform.position);
            yield return null;
        }

        agent.SetDestination(transform.position);
        if (target == null) yield break;

        StartCoroutine(AttackOverTime(target));
    }

    IEnumerator AttackOverTime(Creature target)
    {
        float timer = 0f;

        while(target != null && target.gameObject != null)
        {
            if(timer >= data.AttackSpeed)
            {
                CmdAttackCreature(target.gameObject);
                timer = 0f;
            }

            timer += Time.deltaTime;
            yield return null;
        }
    }

    [Command]
    private void CmdAttackCreature(GameObject target)
    {
        if(target == null) return;

        Creature targetCreature = target.GetComponent<Creature>();

        int damage = Attack; // TODO: Update Damage forumla with defense stat
        int healthAfterAttack = targetCreature.Health - damage;

        
        if (healthAfterAttack <= 0)
        {
            KillCreature(targetCreature);
            Debug.Log("Killed");
        }
        else
        {
            int localAmount = (int)Math.Ceiling(Convert.ToDouble(healthAfterAttack) / Convert.ToDouble(targetCreature.HealthPerUnit));
            targetCreature.CurrentUnitHealth = healthAfterAttack - (targetCreature.HealthPerUnit * Math.Abs(localAmount - 1));
            targetCreature.Amount = localAmount;
            Debug.Log("Amount: " + localAmount + "\nCurrent Unit Health: " + targetCreature.CurrentUnitHealth);
        }
    }

    [Server]
    private void KillCreature(Creature creature)
    {
        var manager = ((FTGNetworkManager)NetworkManager.singleton);
        Player creatureOwner = manager.players[creature.OwnerID];
        creatureOwner.Army.combatArmy.Remove(creature.gameObject);
        NetworkServer.Destroy(creature.gameObject);

        if (creatureOwner.Army.combatArmy.Count <= 0)
        {
            Debug.Log(manager.players[this.OwnerID] + " won!");
            //go back to regular scene, update army slots
        }
    }

    public void Collect(Player player)
    {
        player.Army.AddCreatureToArmy(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //activate stats window
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //deactivate stats window
    }

    public CreatureData Data
    {
        get { return data; }
    }

    public CreatureOutliner Outliner
    {
        get { return outliner; }
    }

    public int ArmySlotIndex
    {
        get { return armySlotIndex; }
        set { armySlotIndex = value; }
    }
}
