using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// will improve attack animation/damage

public class ZombieController : Zombie
{
    protected override void Attack(){
        if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < 0.8f && agent.hasPath)
        {

            if(agent.remainingDistance < 0.10f){
                transform.LookAt(target.gameObject.transform);
            }

            if(Time.time >= lastAttackTime + cooldown){
                agent.isStopped = true;                                                          // stops moving
                if (animations != null) animations.SetBool("Attack", true);                     // attack animation
                target.GetComponent<PlayerStats>().TakeDamage(1f);                              // damage player
                lastAttackTime = Time.time;                                                     // reset cooldown
            }

        }
        else
        {
            agent.isStopped = false; 
            if (animations != null) animations.SetBool("Attack", false);
        }

        if(currentHealth <= 0){
            Die();
        }        
    }

    public override void TakeDamage(float amount)
    {
        if(isDead)
        {
            return;
        }

        currentHealth -= amount;
        print(currentHealth);

    }

    protected override void Die()
    {   
        isDead = true;
        if (animations != null) animations.SetBool("Dead", true);
        Debug.Log("zombie died");
        if(zombieSpawner != null){
            zombieSpawner.decrementZCount();
        }
        Destroy(gameObject, 3f);
    }

}
