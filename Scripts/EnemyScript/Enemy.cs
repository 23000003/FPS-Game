using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //[Header("AI Initialization")]
    private NavMeshAgent agent;
    private Transform target;
    private Animator animations;

    //[Header("Attack Cooldown")]
    private float atkCooldown;
    private float lastAttackTime = 0;               //Store the time when the player was attacked

    //[Header("Zombie Health and Status")]
    private float maxHealth;
    private float currentHealth;
    private bool isDead = false;
    private float damage;
    private int hits = 0;

    private AudioSource audioSource;
    [SerializeField] private AudioClip deadSF;
    [SerializeField] private AudioClip attackSF;
    [SerializeField] private AudioClip baseSF;

    public void SetCurrentHealth(float currentHealth) { this.currentHealth = currentHealth; }
    public void SetAudioSource(AudioSource audioSource) { this.audioSource = audioSource; }
    public void SetAnimator(Animator animations) { this.animations = animations; }
    public void SetAtkCooldown(float cooldown) { this.atkCooldown = cooldown; }
    public void SetMaxHealth(float maxHealth) { this.maxHealth = maxHealth; }
    public void SetTarget(Transform target) { this.target = target; }
    public void SetAgent(NavMeshAgent agent) { this.agent = agent; }
    public void SetDamage(float damage) { this.damage = damage; }
    public void SetHits(int hits) { this.hits = hits; }

    public float GetCurrentHealth() { return this.currentHealth; }
    public Animator GetAnimator() { return this.animations; }
    public float GetMaxHealth() { return this.maxHealth; }
    public int GetHits() { return this.hits; }


    private void TriggerSpecificAudioSource(AudioClip audioClip, bool loop, bool dead)
    {

        if (audioClip != null) 
        {
            audioSource.clip = audioClip;
            audioSource.loop = loop;
            if (!audioSource.isPlaying)
            {

                audioSource.Play();
            }
        }

    }

    protected virtual void Update()
    {
        if (isDead)
        {
            return;
        }

        if (target != null)
        {
            agent.destination = target.position;
            Attack();
        }
    }

    public void Attack()
    {
        if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < 0.8f && agent.hasPath) // this identifies if the enemy is close then attack
        {
            if (agent.remainingDistance < 0.10f)
            {
                transform.LookAt(target.gameObject.transform);
            }

            if (Time.time >= lastAttackTime + atkCooldown)
            {
                agent.isStopped = true;

                TriggerSpecificAudioSource(attackSF, false, false);

                if (animations != null)
                {
                    animations.SetBool("Attack", true);
                }
                hits++;
                Player.Instance.GetStats().TakeDamage(damage); // damage player
                lastAttackTime = Time.time;
                PlayerAttackedAnimation();
            }
        }
        else
        {
            agent.isStopped = false;

            TriggerSpecificAudioSource(baseSF, true, false);
            print("BASEEE");

            if (animations != null)
            {
                animations.SetBool("Attack", false);
            }
        }
    }


    public void TakeDamage(float amount)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy died");

        TriggerSpecificAudioSource(deadSF, false, true);

        isDead = true;

        Player.Instance.GetStats().SetKills(Player.Instance.GetStats().GetKills() + 1);

        if (animations != null)
        {
            animations.SetBool("Dead", true);
        }

        agent.enabled = false;
        GetComponentInChildren<CapsuleCollider>().enabled = false;

        Spawner.Instance.DecrementZCount();

        Destroy(gameObject, 3f);
    }

    private void PlayerAttackedAnimation()
    {
        if (target == null)
            return;

        CanvasGroup bloodEffect = GameObject.FindGameObjectWithTag("BloodEffect").GetComponent<CanvasGroup>();

        StartCoroutine(FadeBloodEffect(bloodEffect, 0f, 0.25f, 0.15f)); // Fade in the blood effect with a smooth transition

        Transform playerTransform = target;

        Quaternion originalRotation = playerTransform.localRotation;

        float flinchAmount = 12f; // Z Axis
        float gravityDuration = 0.15f; // Time duration for the transition effect

        StartCoroutine(FlinchWithGravity(playerTransform, originalRotation, flinchAmount, gravityDuration));
    }

    private IEnumerator FlinchWithGravity(Transform playerTransform, Quaternion originalRotation, float flinchAmount, float duration)
    {
        Vector3 originalEulerAngles = originalRotation.eulerAngles;
        float targetZRotation = originalEulerAngles.z + flinchAmount;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            playerTransform.localRotation = Quaternion.Euler(
                    originalEulerAngles.x, originalEulerAngles.y, Mathf.Lerp(originalEulerAngles.z, targetZRotation, elapsedTime / duration)
                );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerTransform.localRotation = Quaternion.Euler(
                originalEulerAngles.x, originalEulerAngles.y, targetZRotation
            );

        // After the flinch, return to the original rotation smoothly
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            playerTransform.localRotation = Quaternion.Euler(
                    originalEulerAngles.x, originalEulerAngles.y, Mathf.Lerp(targetZRotation, originalEulerAngles.z, elapsedTime / duration)
                 );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerTransform.localRotation = Quaternion.Euler(
                originalEulerAngles.x, originalEulerAngles.y, originalEulerAngles.z
            );

    }

    private IEnumerator FadeBloodEffect(CanvasGroup bloodEffect, float startAlpha, float targetAlpha, float duration)
    {
        bloodEffect.alpha = startAlpha;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            bloodEffect.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bloodEffect.alpha = targetAlpha;

        yield return new WaitForSeconds(0.3f);

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            bloodEffect.alpha = Mathf.Lerp(targetAlpha, 0f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bloodEffect.alpha = 0f;
    }
}
