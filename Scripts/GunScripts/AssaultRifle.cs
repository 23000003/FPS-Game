using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//POLYMORPHISM(?)
public class AssaultRifle : WeaponController
{

    //private WeaponRecoil recoil;
    //private WeaponSway weaponSway;

    public AssaultRifle(float bulletSpeed, float fireRate, float reloadDuration, float reloadTimeRemaining, int bullets, int totalbullets, float reloadTime, float damage)
        : base(bulletSpeed, fireRate, reloadDuration, reloadTimeRemaining, bullets, totalbullets, reloadTime, damage)
    {
    }

    private void Awake()
    {
        //this.recoil = new WeaponRecoil(2f, -2f, 0.35f, 6f, 2f);
        //this.weaponSway = new WeaponSway(0.05f, 0.1f, 6f, transform);
        //this.recoil.GetCamera().enabled = true;
        this.SetRecoilAndSwaySettings(
            new WeaponRecoil(2f, -2f, 0.35f, 6f, 2f),
            new WeaponSway(0.05f, 0.1f, 6f, transform)
        );

        GetWeaponRecoil().GetCamera().enabled = true;
    }

    protected override void Start()
    {
        GetWeaponRecoil().CameraConfig();
        //recoil.CameraConfig();
        base.Start();
        this.SetWeaponSettings(40f, 0.1f, 1.0f, 0f, 30, 200, -1.0f, 20f);
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
        //    GetWeaponRecoil().TriggerRecoilFire();
        //}

    }

    // Start and Update (make it as interface too)

    protected override void SetWeaponSettings(float bulletSpeed, float fireRate, float reloadDuration,
        float reloadTimeRemaining, int bullets, int totalbullets, float reloadTime, float damage)
    {
        base.SetWeaponSettings(bulletSpeed, fireRate, reloadDuration, reloadTimeRemaining, bullets, totalbullets, reloadTime, damage);
    }

    protected override void SetRecoilAndSwaySettings(WeaponRecoil recoil, WeaponSway weaponSway)
    {
        base.SetRecoilAndSwaySettings(recoil, weaponSway);
    }
}
