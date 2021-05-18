using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomedSoul : EnemyAI
{
    Animator animator;
    public PlayerHealth playerHealth;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Patroling()
    {
        base.Patroling();
        animator.SetFloat("speed", 0.5f);
        agent.acceleration = 2;
    }

    protected override void ChasePlayer()
    {
        base.ChasePlayer();
        animator.SetFloat("speed", 1f);
        agent.acceleration = 8;
    }

    protected override void AttackPlayer()
    {
        //base.AttackPlayer();
        agent.SetDestination(transform.position);

        //transform.LookAt(player);
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

        animator.SetFloat("speed", 0f);
        agent.acceleration = 2;

        if (!alreadyAttacked)
        {
            animator.SetBool("attack", true);
            playerHealth.TakeDamage(10);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    protected override void ResetAttack()
    {
        base.ResetAttack();
        animator.SetBool("attack", false);
    }
}
