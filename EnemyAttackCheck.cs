using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAttackCheck : MonoBehaviour
{
    NavMeshAgent agent;

    float movespeedReset;
    float attackingSpeed = 0;

    bool endingAtk;
    bool attacking;
    public float timeTilAtk;
    float timeTilAtkReset;
    public float atkRecoveryTime;
    float atkRecoveryTimeReset;

    public LayerMask canAttack;
    public Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        movespeedReset = agent.speed;
        timeTilAtkReset = timeTilAtk;
        atkRecoveryTimeReset = atkRecoveryTime;
    }

    // Update is called once per frame
    void Update()
    {
        #region waiting for attack and attack
        if (attacking)
        {
            if(timeTilAtk>0)
            {
                timeTilAtk -= Time.deltaTime;
            }
            else
            {
                //make the attack, this is temp
                if(Physics.BoxCast(transform.position, transform.localScale * .6f, transform.forward, transform.rotation, canAttack))
                {
                    Debug.Log("player hit");
                }
                //move onto next phase of attack
                anime.SetTrigger("Stab Attack");
                attacking = false;
                endingAtk = true;
                timeTilAtk = timeTilAtkReset;
            }
        }
        #endregion

        #region after attack behavior
        if (endingAtk)
        {
            if(atkRecoveryTime>0)
            {
                atkRecoveryTime -= Time.deltaTime;
            }
            else
            {
                atkRecoveryTime = atkRecoveryTimeReset;
                endingAtk = false;
                agent.speed = movespeedReset;
            }
        }
        #endregion
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && !attacking && !endingAtk)
        {
            Debug.Log("ayaya");
            agent.speed = attackingSpeed;
            attacking = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, transform.localScale * 2);
        
    }
}
