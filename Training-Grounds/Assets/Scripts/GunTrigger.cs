using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTrigger : MonoBehaviour
{

    //Empty totalbullets functionality soon 

    // Reference Objects
    private Animator animations;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ParticleSystem flash;

    [SerializeField] private AudioClip fireClip;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private AudioSource fireSource;
    [SerializeField] private AudioSource reloadSource;
   
    // will use a Setter make it private
    private float bulletSpeed;
    private float fireRate; 
    private float reloadDuration;
    private float reloadTimeRemaining;
    private int bullets;
    private int totalbullets;

    //This is not a constructor but a function setter :))
    protected void GunSettings(float bulletSpeed, float fireRate, float reloadDuration,
                            float reloadTimeRemaining, int bullets, int totalbullets)
    {
        this.bulletSpeed = bulletSpeed;
        this.fireRate = fireRate;
        this.reloadDuration = reloadDuration;
        this.reloadTimeRemaining = reloadTimeRemaining;
        this.bullets = bullets;
        this.totalbullets = totalbullets;
        tempBullets = bullets;
    }

    private float nextFireTime = 0f;
    private bool Reload = false;
    private bool isReloading = false;
    private int tempBullets;

    protected virtual void Start()
    {
        // Access to the animations controller
        animations = gameObject.GetComponent<Animator>();
        animations.SetInteger("Movement", 0);
        InstantiateAudio(fireClip, reloadClip);
    }

    protected virtual void Update()
    {
        HandleShooting();
        HandleReloading();
        GunPerspectiveMovements();
    }

    private void InstantiateAudio(AudioClip Fireclip, AudioClip reloadClip)
    {
        fireSource = gameObject.AddComponent<AudioSource>();
        fireSource.clip = Fireclip;

        reloadSource = gameObject.AddComponent<AudioSource>();
        reloadSource.clip = reloadClip;
    }

    private void HandleShooting()
    {
        // if left click is on hold by user
        if (Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftShift)) 
        {
            // if enough time has passed to fire again
            if (Time.time > nextFireTime && bullets > 0)
            {
                // Instantiate the bullet (Fire)
                var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.forward * bulletSpeed;
                
                // Gun Fire Sound Effects
                fireSource.PlayOneShot(fireClip);

                // Particle Effect
                flash.Play();

                // Set the time for the next shot
                nextFireTime = Time.time + fireRate;
    
                if (Input.GetMouseButtonDown(0)) // 1 tap
                {
                    animations.SetInteger("Fire", 1);
                }
                else
                {
                    animations.SetInteger("Fire", 2);
                }

                animations.SetInteger("Movement", -1);

                --bullets;

            }
            else if(bullets <= 0)
            {
                Reload = true;
                print("TRUE");
            }
        }
        else
        {
            animations.SetInteger("Fire", -1);
        }
    }

    private void HandleReloading()
    {
        bool ManualReload = Input.GetKey(KeyCode.R);
        if (Reload || (ManualReload && bullets != 30))
        {
            if (!isReloading)
            {
                // Start the reload process
                isReloading = true;
                reloadTimeRemaining = reloadDuration;
                Console.WriteLine("Reloading");
                reloadSource.PlayOneShot(reloadClip);
                animations.SetInteger("Reload", 0);
            }
        }

        if (isReloading)
        {
            reloadTimeRemaining -= Time.deltaTime;
           
            if (reloadTimeRemaining <= -1.0f)
            {
                // Reload complete
                animations.SetInteger("Reload", -1);
                isReloading = false;
                Reload = false;
                totalbullets -= tempBullets - bullets;
                bullets = tempBullets;
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

        bool Scope = Input.GetKey(KeyCode.Mouse1);

        if (Scope && !Input.GetKey(KeyCode.LeftShift))
        {
            animations.SetBool("Sight", true);
        }
        else
        {
            animations.SetBool("Sight", false);
        }
    }

    //Encapsulation
    public string GetAmmo()
    {
        return bullets.ToString();
    }

    public string GetTotalAmmo()
    {
        return totalbullets.ToString();
    }


}



