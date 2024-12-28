using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundEffects
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip deadSF;
    [SerializeField] private AudioClip attackSF;
    [SerializeField] private AudioClip baseSF;

    public EnemySoundEffects(AudioSource audioSource, AudioClip deadSF, AudioClip attackSF, AudioClip baseSF)
    {
        this.audioSource = audioSource;
        this.deadSF = deadSF;
        this.attackSF = attackSF;
        this.baseSF = baseSF;
    }

    public void SetAudioSource(AudioSource audioSource) { this.audioSource = audioSource; }

    public void TriggerSpecificAudioSource(AudioClip audioClip, bool loop, bool dead)
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
}
