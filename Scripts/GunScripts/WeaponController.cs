using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponController : Weapon
{

    // Reference Objects
    private Animator animations;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject flash;

    [SerializeField] private AudioClip fireClip;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private AudioSource fireSource;
    [SerializeField] private AudioSource reloadSource;

    [SerializeField] private GameObject cameraRayCast;
    [SerializeField] private GameObject bulletOnStoneEffect;
    [SerializeField] private GameObject bulletOnTargetEffect;

    private float nextFireTime = 0f;
    private bool Reload = false;
    private bool isReloading = false;

    public WeaponController(float bulletSpeed, float fireRate, float reloadDuration, float reloadTimeRemaining, int bullets, int totalbullets, float reloadTime, float damage)
        : base(bulletSpeed, fireRate, reloadDuration, reloadTimeRemaining, bullets, totalbullets, reloadTime, damage)
    {
    }

    protected virtual void SetRecoilAndSwaySettings(WeaponRecoil recoil, WeaponSway weaponSway)
    {
        SetWeaponSway(weaponSway);
        SetWeaponRecoil(recoil);
    }
    public void HEY()
    {
        throw new System.NotImplementedException();
    }

    protected virtual void SetWeaponSettings(float bulletSpeed, float fireRate, float reloadDuration,
                            float reloadTimeRemaining, int bullets, int totalbullets, float reloadTime, float damage)
    {
        SetBullets(bullets);
        SetBulletSpeed(bulletSpeed);
        SetFireRate(fireRate);
        SetReloadDuration(reloadDuration);
        SetReloadTimeRemaining(reloadTimeRemaining);
        SetTempBullets(bullets);
        SetBullets(bullets);
        SetTotalBullets(totalbullets);
        SetTempTotalBullets(totalbullets);
        SetReloadTime(reloadTime);
        SetWeaponDamage(damage);
    }

    protected virtual void Start()
    {
        // Access to the animations controller
        animations = gameObject.GetComponent<Animator>();
        animations.SetInteger("Movement", 0);
        InstantiateAudio(fireClip, reloadClip);
    }

    protected virtual void Update()
    {
        Instance = this;
        HandleShooting();
        HandleReloading();
        GunPerspectiveMovements();
    }

    private void InstantiateAudio(AudioClip Fireclip, AudioClip reloadClip)
    {
        fireSource = gameObject.AddComponent<AudioSource>();
        fireSource.playOnAwake = false;
        fireSource.clip = Fireclip;

        reloadSource = gameObject.AddComponent<AudioSource>();
        reloadSource.playOnAwake = false;
        reloadSource.clip = reloadClip;
    }

    private void HandleShooting()
    {
        // if left click is on hold by user
        if (Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftShift) && !GameState.Instance.IsGameOver())
        {
            // if enough time has passed to fire again
            if (Time.time > nextFireTime && !Reload && GetBullets() > 0)
            {

                //Bullets/Fire Effects
                RaycastHit fireGun;
                if (Physics.Raycast(cameraRayCast.transform.position, cameraRayCast.transform.forward, out fireGun))
                {

                    //Detect if there is a zombie in the range of the fire path
                    Enemy zombie = fireGun.collider.GetComponentInParent<Enemy>();
                    //If the collider tag is a zombie do not summon bullet effect
                    /*
                        added additional condition for player because for some reason when the player
                        because for some reason when the player moves backward the player gets hit
                        maybe has something to do with the fact that the player has 4 arms
                        ikaw lay ayo ani
                    */
                    if (zombie != null || fireGun.collider.tag == "Enemy")
                    {
                        // Make the zombie take damage
                        print("zombie hit");

                        zombie.TakeDamage(GetWeaponDamage());
                        // will change the effect to blood
                 
                        GameObject bloodEffect = Instantiate(bulletOnTargetEffect, fireGun.point, Quaternion.LookRotation(fireGun.normal));
                        bloodEffect.transform.parent = zombie.transform;
                    }
                    else
                    {
                        //Only summon bullet effect if the collider tag is not a zombie
                        Instantiate(bulletOnStoneEffect, fireGun.point, Quaternion.LookRotation(fireGun.normal));
                    }
                }


                // Gun Fire Sound Effects
                fireSource.PlayOneShot(fireClip);

                // Particle Effect
                GameObject flashTrigger = Instantiate(flash, bulletSpawn.position, bulletSpawn.rotation);
                flashTrigger.transform.parent = bulletSpawn.transform;

                Destroy(flashTrigger, 0.18f);

                // Set the time for the next shot
                nextFireTime = Time.time + GetFireRate();

                //recoil
                GetWeaponRecoil().TriggerRecoilFire();
                //isFiring = true;

                if (Input.GetMouseButtonDown(0)) // 1 tap
                {
                    animations.SetInteger("Fire", 1);
                }
                else
                {
                    animations.SetInteger("Fire", 2);
                }

                animations.SetInteger("Movement", -1);

                SetBullets(GetBullets() - 1);

            }
            else if (GetBullets() <= 0 && GetTotalbullets() > 0)
            {
                Reload = true;
                
            }
        }
        else
        {
            animations.SetInteger("Fire", -1);
        }

        if (Player.Instance.GetPlayerMovements().GetIsOnGround())
        {
            animations.SetFloat("Run", 1.3f);
            print("HERE");
        }
        else
        {
            animations.SetFloat("Run", 0.5f);
            print("HERE1");

        }
    }

    private void HandleReloading()
    {
        bool ManualReload = Input.GetKey(KeyCode.R);
        if (Reload || (ManualReload && GetBullets() != GetTempBullets() && GetTotalbullets() > 0))
        {
            if (!isReloading)
            {
                // Start the reload process
                isReloading = true;
                //reloadTimeRemaining = reloadDuration;
                SetReloadTimeRemaining(GetReloadDuration());
                Console.WriteLine("Reloading");
                reloadSource.PlayOneShot(reloadClip);
                animations.SetInteger("Reload", 0);
                print("HERE 1");
            }
        }
        if (isReloading)
        {
            //reloadTimeRemaining -= Time.deltaTime;
            SetReloadTimeRemaining(GetReloadTimeRemaining() - Time.deltaTime);

            if (GetReloadTimeRemaining() <= GetReloadTime())
            {
                print("HERE 3");

                // Reload complete
                animations.SetInteger("Reload", -1);
                isReloading = false;
                Reload = false;


                int tempBullets = GetTempBullets() - GetBullets();
                int tempTotalBullets = GetTotalbullets() - tempBullets >= 0 ? GetTotalbullets() - tempBullets : -1;
                
                SetBullets(tempTotalBullets == -1 ? GetBullets() + GetTotalbullets() : GetTempBullets());
                SetTotalBullets(tempTotalBullets == -1 ? 0 : tempTotalBullets);
            }
        }
    }

    private void GunPerspectiveMovements()
    {
        bool idenMovement = Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0;
        bool Run = Input.GetKey(KeyCode.LeftShift);

        if (Run)
        {
            animations.SetInteger("Movement", 2);
        }
        else
        {
            animations.SetInteger("Movement", (idenMovement ? 0 : 1));
        }

        bool Scope = Input.GetKey(KeyCode.Mouse1); // My mouse rightclick is broken, just change to Mouse1(rightClick)

        if (Scope && !Input.GetKey(KeyCode.LeftShift))
        {
            animations.SetBool("Sight", true);
        }
        else
        {
            animations.SetBool("Sight", false);
        }
    }


}



