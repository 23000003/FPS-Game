using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Zombie : MonoBehaviour
{
    [Header("AI Initialization")]
    [SerializeField] protected NavMeshAgent agent;
    protected Transform target;
    protected Animator animations;

    [Header("Attack Cooldown")]
    [SerializeField] protected float cooldown = 5f;
    protected float lastAttackTime;               //Store the time when the player was attacked

    [Header("Zombie Health and Status")]
    [SerializeField] protected float maxHealth;
    protected float currentHealth;
    protected bool isDead = false;

    [Header("Zombie Spwaner Reference")]
    protected Spawner zombieSpawner;

    /*
        Virtual is basically the same as abstract but it allows you to put an implementaion
        on the method.

        It is set to virtual since the default might just be fine for the other children but
        also allows to be overridden in case you want a different implementation overall
    */
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animations = GetComponentInChildren<Animator>();
        lastAttackTime =- cooldown;
        currentHealth = maxHealth;
        animations.Play("Run");
        //Ensures smooth transition when the zombie spawns in to the scene

        /*
            Instead of having to initialize the player in the scene itself we can just let the function
            find the player for us using the FindWithTag method
        */
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        zombieSpawner = FindObjectOfType<Spawner>();
    }

    protected virtual void Update()
    {   
        if(isDead)
        {
            return;
        }

        if (target != null)
        {
            agent.SetDestination(target.position);
            Attack();
        }

    }

    protected abstract void Attack();

    public abstract void TakeDamage(float amount);

    protected abstract void Die();
  
}

