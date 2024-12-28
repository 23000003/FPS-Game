using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Boss : Enemy
{
    private bool activateBuff = false;

    private void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        //animations = GetComponentInChildren<Animator>();
        //lastAttackTime = -cooldown;
        //animations.Play("Run");

        SetAudioSource(GetComponent<AudioSource>());
        SetAgent(GetComponent<NavMeshAgent>());
        SetAnimator(GetComponentInChildren<Animator>());
        SetAtkCooldown(2f);
        GetAnimator().Play("Run");

        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            //target = player.transform;
            SetTarget(player.transform);
        }

        SetMaxHealth(1000);
        SetCurrentHealth(1000);
        SetDamage(20f);
    }

    protected override void Update()
    {
        base.Update();
        print(GetCurrentHealth());
        if(GetCurrentHealth() < 200 || activateBuff)
        {
            print("BUFF ACTIVATED");
            activateBuff = true;
            ApplyBuff(30f, 1.5f, 50f);
            GetAnimator().SetFloat("AttackSpeed", 0.58f);
        }
        else
        {
            GetAnimator().SetFloat("AttackSpeed", 0.40f);
        }
    }

    private void ApplyBuff(float Damage, float atkCooldown, float heal)
    {
        SetDamage(Damage);
        SetAtkCooldown(atkCooldown);

        if (GetHits() % 4 == 0) 
        {
            if (GetCurrentHealth() <= GetMaxHealth()) 
            {
                float temp = GetCurrentHealth() + heal >= GetMaxHealth() ? GetMaxHealth() : GetCurrentHealth() + heal;
                SetCurrentHealth(temp);
                print(GetCurrentHealth() + "LIFESTEAL");
            }
            SetHits(1); // if 0 then it will rappidly active the condition
        }

    }

}
