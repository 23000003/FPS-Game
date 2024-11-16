using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponController
{
    //private WeaponRecoil recoil;
    //private WeaponSway weaponSway;

    public Pistol(float bulletSpeed, float fireRate, float reloadDuration, float reloadTimeRemaining, int bullets, int totalbullets, float reloadTime)
        : base(bulletSpeed, fireRate, reloadDuration, reloadTimeRemaining, bullets, totalbullets, reloadTime)
    {
    }

    private void Awake()
    {
        //this.recoil = new WeaponRecoil(
        //    1.5f,    // recoilX
        //    1.5f,    // recoilY (reduce this if vertical recoil is too strong)
        //    0.25f, // recoilZ
        //    9f,    // snappiness
        //    1f     // returnSpeed
        //);
        //this.weaponSway = new WeaponSway(0.05f, 0.1f, 6f, transform);

        //this.recoil.GetCamera().enabled = true;

        this.SetRecoilAndSwaySettings(
            new WeaponRecoil(1.5f, 1.5f, 0.25f, 9f, 1f),
            new WeaponSway(0.05f, 0.1f, 6f, transform)
        );
        GetWeaponRecoil().GetCamera().enabled = true;
    }

    protected override void Start()
    {
        //recoil.CameraConfig();
        GetWeaponRecoil().CameraConfig();
        base.Start(); // to start its inherited functionality
        this.SetWeaponSettings(15f, 0.15f, 1.5f, 0f, 14, 124, -0.1f);
    }

    protected override void Update()
    {
        base.Update();
        GetWeaponRecoil().UpdateRecoilPosition();
        GetWeaponSway().UpdateSway();

        //recoil.UpdateRecoilPosition();
        //weaponSway.UpdateSway();

        //if (GetIsFiring())
        //{
        //    recoil.TriggerRecoilFire();
        //}
    }

    // Start and Update (make it as interface too)

    protected override void SetWeaponSettings(float bulletSpeed, float fireRate, float reloadDuration,
        float reloadTimeRemaining, int bullets, int totalbullets, float reloadTime)
    {
        base.SetWeaponSettings(bulletSpeed, fireRate, reloadDuration, reloadTimeRemaining, bullets, totalbullets, reloadTime);
    }

    protected override void SetRecoilAndSwaySettings(WeaponRecoil recoil, WeaponSway weaponSway)
    {
        base.SetRecoilAndSwaySettings(recoil, weaponSway);
    }

}
