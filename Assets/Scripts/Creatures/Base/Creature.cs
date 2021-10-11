using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using System;

public abstract class Creature : NetworkBehaviour, ICollectable
{
    [SerializeField] [SyncVar] private CreatureData data;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    private int armySlotIndex = -1;

    bool isAttacking = false;

    public virtual void Attack(Creature target) //kinda auto attacks
    {
        StartCoroutine(MoveToTarget(target));
    }
    public virtual void SpecialAttack(Creature target) // big bonanza attack dependent on the creature
    {
        StartCoroutine(MoveToTarget(target));
    }

    IEnumerator MoveToTarget(Creature target)
    {
        GameObject targetObj = target.gameObject;

        while(data.AttackRange <= Vector3.Distance(transform.position, target.transform.position))
        {
            if(targetObj != null)
            {
                agent.SetDestination(target.transform.position);
                yield return null;
            }
            else
            {
                agent.SetDestination(transform.position);
                yield break;
            }
        }

        agent.SetDestination(transform.position);
        StartCoroutine(AttackOverTime(target));
    }

    IEnumerator AttackOverTime(Creature target)
    {
        while(target.gameObject != null)
        {
            CmdAttackCreature(target.gameObject);
            yield return new WaitForSeconds(data.AttackSpeed);
        }
    }

    [Command]
    private void CmdAttackCreature(GameObject target)
    {
        Creature targetCreature = target.GetComponent<Creature>();
        targetCreature.data.Hp -= data.Attack;
        Debug.Log(targetCreature.data.Hp);

        if(targetCreature.data.Hp == 0)
        {
            NetworkServer.UnSpawn(target);
        }
        //later make this more complex based on how many units it has, hp defense etc
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
    }
}
