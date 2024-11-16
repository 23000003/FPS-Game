using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public static Weapon Instance { get; set; }

    private float bulletSpeed;
    private float fireRate;
    private float reloadDuration;
    private float reloadTimeRemaining;
    private int bullets;
    private int totalbullets;
    private int tempBullets;
    private int tempTotalBullets;
    private float reloadTime;

    private WeaponRecoil recoil;
    private WeaponSway weaponSway;

    public void SetWeaponRecoil(WeaponRecoil recoil) { this.recoil = recoil; }
    public WeaponRecoil GetWeaponRecoil() { return recoil; }
    public void SetWeaponSway(WeaponSway weaponSway) { this.weaponSway = weaponSway; }
    public WeaponSway GetWeaponSway() { return weaponSway; }


    public Weapon(float bulletSpeed, float fireRate, float reloadDuration,
                            float reloadTimeRemaining, int bullets, int totalbullets, float reloadTime)
    {
        this.bulletSpeed = bulletSpeed;
        this.fireRate = fireRate;
        this.reloadDuration = reloadDuration;
        this.reloadTimeRemaining = reloadTimeRemaining;
        this.bullets = bullets;
        this.totalbullets = totalbullets;
        this.reloadTime = reloadTime;
        tempBullets = bullets;
        tempTotalBullets = totalbullets;
    }

    public float GetBulletSpeed() {  return bulletSpeed; }
    public float GetFireRate() { return fireRate; }
    public float GetReloadDuration() { return reloadDuration; }
    public float GetReloadTimeRemaining() { return reloadTimeRemaining; }
    public int GetBullets() {   return bullets; }   
    public int GetTotalbullets() { return totalbullets; }
    public int GetTempBullets() {return tempBullets; }
    public float GetReloadTime() { return reloadTime; }
    public int GetTempTotalBullets() { return tempTotalBullets; }

    public void SetBulletSpeed(float bulletSpeed) {  this.bulletSpeed = bulletSpeed; }
    public void SetFireRate(float fireRate) {  this.fireRate = fireRate; }
    public void SetReloadDuration(float reloadDuration) {  this.reloadDuration = reloadDuration; }
    public void SetReloadTimeRemaining(float reloadTimeRemaining) {  this.reloadTimeRemaining = reloadTimeRemaining; }
    public void SetBullets(int bullets) {  this.bullets = bullets; }
    public void SetTotalBullets(int totalBullets) {  this.totalbullets = totalBullets; }
    public void SetTempBullets(int tempBullets) { this.tempBullets = tempBullets; }
    public void SetReloadTime(float reloadTime) {  this.reloadTime = reloadTime; }
    public void SetTempTotalBullets(int totalBullets) { this.tempTotalBullets = totalBullets; }

    protected abstract void InstantiateAudio(AudioClip Fireclip, AudioClip reloadClip);
}
