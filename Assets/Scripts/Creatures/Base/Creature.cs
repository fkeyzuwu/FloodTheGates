using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using System;

public abstract class Creature : NetworkBehaviour, ICollectable
{
    [SyncVar] public int Hp;
    [SyncVar] public int Amount = 1; //default

    [SyncVar] public int OwnerID = -1;
    [SerializeField] [SyncVar] private CreatureData data;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SyncVar] private int armySlotIndex = -1;

    //bool isAttacking = false;

    void Start()
    {
        Hp = data.HpPerUnit;
    }

    public virtual void Attack(Creature target) //kinda auto attacks
    {
        StopAllCoroutines();
        StartCoroutine(MoveToTarget(target));
    }
    public virtual void SpecialAttack(Creature target) // big bonanza attack dependent on the creature
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
        targetCreature.Hp -= data.Attack;
        //later make this more complex based on how many units it has, hp defense etc
        Debug.Log(targetCreature.Hp);

        if(targetCreature.Hp <= 0)
        {
            Player targetOwner = ((FTGNetworkManager)NetworkManager.singleton).players[targetCreature.OwnerID];
            targetOwner.Army.combatArmy.Remove(target);
            Debug.Log(targetOwner.Army.combatArmy.Count);

            if (targetOwner.Army.combatArmy.Count <= 0)
            {
                Debug.Log(((FTGNetworkManager)NetworkManager.singleton).players[this.OwnerID] + " won!");
                //go back to regular scene, update army slots
            }
            NetworkServer.Destroy(target);
        }
        
    }

    public void Collect(Player player)
    {
        player.Army.AddCreatureToArmy(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public CreatureData Data
    {
        get { return data; }
    }

    public int ArmySlotIndex
    {
        get { return armySlotIndex; }
        set { armySlotIndex = value; }
    }
}
