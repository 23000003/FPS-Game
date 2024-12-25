using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEffects
{
    private readonly GameObject footstepSF;
    private readonly GameObject sprintSF;
    private readonly GameObject jumpSF;
    private readonly GameObject pickUpSF;
    private readonly MonoBehaviour mono;
    private bool isRunning = false;
    private bool isWalking = false;
    private bool isJumping = false;
    private bool jumpHelper = false;

    public PlaySoundEffects(GameObject footstepSF, GameObject sprintSF, GameObject jumpSF, GameObject pickUpSF) 
    {
        this.footstepSF = footstepSF;
        this.sprintSF = sprintSF;   
        this.jumpSF = jumpSF;
        this.pickUpSF = pickUpSF;
    }

    
    public void ConfigureSoundEffects()
    {
        footstepSF.SetActive(false);
        sprintSF.SetActive(false);
        //jumpSF.SetActive(false);
        //pickUpSF.SetActive(false);
    }


    public void SoundEffectsMethods(bool isOnGround, bool isPickedUp)
    {

        isJumping = !isOnGround;
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isWalking = 
                Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D) ||
                Input.GetKey(KeyCode.A);

        try
        {
            if (isPickedUp)
            {
                pickUpSF.GetComponent<AudioSource>().Play();
            }


            if (isJumping)
            {
                footstepSF.SetActive(false);
                sprintSF.SetActive(false);
                jumpHelper = true;
                Console.WriteLine("Jumped");
            }
            else if (isRunning && isWalking && !isJumping)
            {
                sprintSF.SetActive(true);
                footstepSF.SetActive(false);
            }
            else if (isWalking && !isJumping && !isRunning)
            {
                footstepSF.SetActive(true);
                sprintSF.SetActive(false);
            }
            else
            {
                footstepSF.SetActive(false);
                sprintSF.SetActive(false);
            }

            if (jumpHelper) // if it hits the ground after jumping then play audio
            {
                if (!isJumping)
                {
                    jumpSF.GetComponent<AudioSource>().Play();
                    jumpHelper = false;
                    Console.WriteLine("DOneJumped");
                }
                Console.WriteLine("NotInIsJumping");
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

}
