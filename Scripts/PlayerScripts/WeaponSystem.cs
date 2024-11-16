using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public static WeaponSystem Instance { get; set; }

    private GameObject[] getWeapons;
    private WeaponSwitching weaponSwitching;
    private WeaponPickUp weaponPickUp; 
    
    public WeaponSwitching GetWeaponSwitching() { return weaponSwitching; }

    private void Awake() //public void ConfigureObjectsOnAwake
    {
        Instance = this;

        getWeapons = GameObject.FindGameObjectsWithTag("Weapon");

        //for (int i = 0; i < temp.Length; i++)
        //{
        //    if (temp[i].name == "Assault Rifle") getWeapons[0] = temp[i];
        //    if (temp[i].name == "Submaching Gun") getWeapons[1] = temp[i];
        //    if (temp[i].name == "Maching Gun") getWeapons[2] = temp[i];
        //}

        GameObject secondary = GameObject.FindGameObjectWithTag("2ndWeapon");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        weaponPickUp = new WeaponPickUp(getWeapons);

        if (getWeapons != null && secondary != null && player != null)
        {
            weaponSwitching = new WeaponSwitching(getWeapons, secondary, weaponPickUp);
        }
        else
        {
            print("No gameobjects found");
        }
    }

    private void Start() // public void SetObjectsActiveInactive
    {
        weaponSwitching.GetPrimary()[0].SetActive(true);
        weaponSwitching.GetPrimary()[1].SetActive(false);
        weaponSwitching.GetPrimary()[2].SetActive(false);
        weaponSwitching.GetSecondary().SetActive(false);
    }

    private void Update() // public void UpdateWeaponSystem
    {
        if (weaponSwitching.GetIsPrimary())
        {
            weaponPickUp.PickUpWeapon();
        }

        weaponSwitching.TriggerSwitchWeapon();

        FillAmmunation();

    }

    private void FillAmmunation()
    {
        // If ammobox is interacted
        if (PlayerInteraction.Instance.GetObjectTag() == "AmmoBox" && Input.GetKeyDown(KeyCode.F))
        {
            WeaponController.Instance.SetBullets(WeaponController.Instance.GetTempBullets());
            WeaponController.Instance.SetTotalBullets(WeaponController.Instance.GetTempTotalBullets());
        }
    }

}


// *********** Prolly gonna transfer gunSystem to Playerscripts and
// *********** Just create a Getter function to set the AmmoText 
// *********** And Create a FillAmmoFunction on it
//
// late binding, error handling
// ========= SINGLETON .... Problem: Fetching the current value of an object