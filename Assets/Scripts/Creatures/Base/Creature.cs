using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using System;

public abstract class Creature : NetworkBehaviour, ICollectable
{
    public int Hp;
    public int Amount = 1; //default

    private Army ownerArmy;
    [SerializeField] [SyncVar] private CreatureData data;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    private int armySlotIndex = -1;

    bool isAttacking = false;

    void Start()
    {
        Hp = data.HpPerUnit;
    }

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
        Debug.Log(targetCreature);

        if(targetCreature.Hp <= 0)
        {
            //need to remove from army some time, maybe at the end of battle? idk nigga
            targetCreature.ownerArmy.combatArmy.Remove(target);
            if (targetCreature.ownerArmy.combatArmy.Count > 0)
            {
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
    }

    public Army OwnerArmy
    {
        get { return ownerArmy; }
        set { ownerArmy = value; }
    }
}
