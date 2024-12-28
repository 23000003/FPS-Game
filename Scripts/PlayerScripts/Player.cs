using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private WeaponSystem weaponSystem;
    private PlayerMovements playerMovements;
    private PlayerInteraction playerInteraction;
    private PlaySoundEffects playSoundEffects;
    private PlayerStats stats;

    [SerializeField] private GameObject footstepSF;
    [SerializeField] private GameObject sprintSF;
    [SerializeField] private GameObject jumpSF;
    [SerializeField] private GameObject pickUpSF;

    public WeaponSystem GetWeaponSystem() {  return weaponSystem; }
    public PlayerInteraction GetPlayerInteraction() { return playerInteraction; }
    public PlayerStats GetStats() { return stats; }
    public PlayerMovements GetPlayerMovements() { return playerMovements; }

    private void Awake()
    {
        Instance = this;

        try
        {
            playerMovements = new PlayerMovements
                   (
                       GetComponent<CharacterController>(),
                       GameObject.FindGameObjectWithTag("WeaponCamera").GetComponent<Camera>(),
                       transform
                   );

            weaponSystem = new WeaponSystem();
            weaponSystem.ConfigureObjectsOnAwake();

            playerInteraction = new PlayerInteraction(3, GameObject.FindGameObjectWithTag("CameraRecoil"), "");

            playSoundEffects = new PlaySoundEffects(footstepSF, sprintSF, jumpSF, pickUpSF);

            stats = new PlayerStats(200);
        
        }
        catch(NullReferenceException e)
        {
            Debug.Log(e.ToString());
        }
    }

    private void Start()
    {
        weaponSystem.SetObjectsActiveInactive();
        playerMovements.ConfigCursor();
        playSoundEffects.ConfigureSoundEffects();
        stats.ConfigureStats();
    }

    private void Update()
    {
        Instance = this;

        weaponSystem.UpdateWeaponSystem();
        playerMovements.MovementController();
        playerInteraction.CheckInteraction();
        playerInteraction.QuestObjectInteraction();
        playSoundEffects.SoundEffectsMethods(playerMovements.GetIsOnGround(), playerInteraction.GetIsPickedUp());
        stats.UpdateHealth();

        print(playerMovements.GetIsOnGround());
    }

}


