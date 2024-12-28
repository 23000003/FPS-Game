using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Enemy
{

    private void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        //animations = GetComponentInChildren<Animator>();
        //lastAttackTime = -cooldown;
        //animations.Play("Run");
        SetAudioSource(GetComponent<AudioSource>());
        SetAgent(GetComponent<NavMeshAgent>());
        SetAnimator(GetComponentInChildren<Animator>());
        SetAtkCooldown(1f);
        GetAnimator().Play("Run");

        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            //target = player.transform;
            SetTarget(player.transform);
        }

        SetMaxHealth(50);
        SetCurrentHealth(50);
        SetDamage(5f);
    }

    protected override void Update()
    {
        base.Update();
    }
}
