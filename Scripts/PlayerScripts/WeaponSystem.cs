using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem
{
    private GameObject[] getWeapons = new GameObject[3];
    private WeaponSwitching weaponSwitching;
    private WeaponPickUp weaponPickUp;

    public WeaponSystem()
    {
    }
    public WeaponSwitching GetWeaponSwitching() { return weaponSwitching; }
    public WeaponPickUp GetWeaponPickUp() { return weaponPickUp; }

    public void ConfigureObjectsOnAwake()
    {
        try
        {

            AssignPrimaryWeapons();

            // Find the secondary weapon and player
            GameObject secondary = GameObject.FindGameObjectWithTag("2ndWeapon");
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // Check if secondary weapon or player are not found
            if (secondary == null)
            {
                throw new NullReferenceException("No secondary weapon found in the scene.");
            }

            if (player == null)
            {
                throw new NullReferenceException("No player found in the scene.");
            }

            weaponPickUp = new WeaponPickUp(getWeapons);
            weaponSwitching = new WeaponSwitching(getWeapons, secondary, weaponPickUp);

        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogError("IndexOutOfRangeException: " + e.Message);
        }
        catch (NullReferenceException e)
        {
            Debug.LogError("NullReferenceException: " + e.Message);
        }
        catch (Exception e)
        {
            Debug.LogError("An error occurred: " + e.Message);
        }
    }

    public void AssignPrimaryWeapons()
    {

        GameObject[] tempWeapons = GameObject.FindGameObjectsWithTag("Weapon");
        GameObject[] tempPickUpWeapons = GameObject.FindGameObjectsWithTag("GameObject");

        if(tempWeapons == null || tempPickUpWeapons == null)
        {
            throw new NullReferenceException("No Weapon or Gameobject found in the scene");
        }

        for (int i = 0; i < tempWeapons.Length; i++)
        {
            //if (temp[i].name == "Assault Rifle") getWeapons[0] = temp[i];
            //if (temp[i].name == "Submachine Gun") getWeapons[1] = temp[i];
            //if (temp[i].name == "Machine Gun") getWeapons[2] = temp[i];

            for (int j = 0; j < tempPickUpWeapons.Length; j++)
            {
                if (tempPickUpWeapons[j].name == tempWeapons[i].name)
                {
                    getWeapons[tempPickUpWeapons[j].
                        GetComponent<InteractGameObjects>().GetIndex()] = tempWeapons[i];
                }
            }
        }
    } 


    public void SetObjectsActiveInactive()
    {
        try
        {
            weaponSwitching.GetPrimary()[0].SetActive(true);

            for (int i = 1; i < getWeapons.Length; i++)
            {
                weaponSwitching.GetPrimary()[i].SetActive(false);
            }

            weaponSwitching.GetSecondary().SetActive(false);
        }
        catch(IndexOutOfRangeException e)
        {
            Debug.LogError("IndexOutOfRangeException: " + e.Message);
        }
    }


    public void UpdateWeaponSystem() 
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
        if (Player.Instance.GetPlayerInteraction().GetObjectTag() == "AmmoBox" && Input.GetKeyDown(KeyCode.F))
        {
            WeaponController.Instance.SetBullets(WeaponController.Instance.GetTempBullets());
            WeaponController.Instance.SetTotalBullets(WeaponController.Instance.GetTempTotalBullets());
        }
    }

}