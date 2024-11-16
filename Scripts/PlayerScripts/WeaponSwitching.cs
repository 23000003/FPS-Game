using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching
{
    private GameObject[] primary;
    private GameObject secondary;
    private readonly WeaponPickUp currGun;
    private bool isPrimaryActive = true;
    private bool isSecondaryActive = false;

    public WeaponSwitching(GameObject[] primary, GameObject secondary, WeaponPickUp gun)
    {
        this.primary = primary;
        this.secondary = secondary;
        this.currGun = gun;
    }

    public GameObject[] GetPrimary() { return primary; }
    public GameObject GetSecondary() { return secondary; }
    public bool GetIsPrimary() { return isPrimaryActive; }
    public bool GetIsSecondary() { return isSecondaryActive; }

    private readonly float switchTime = 1f; // Time in seconds for switching weapons
    private float switchTimer = 0f;
    private bool isSwitching = false;

    public void TriggerSwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && primary[currGun.GetCurrGunActive()].activeSelf && !isSwitching)
        {
            primary[currGun.GetCurrGunActive()].GetComponent<Animator>().SetTrigger("TakeOut");
            switchTimer = 0f; // Reset the timer
            isSwitching = true; // Start the switching process
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && secondary.activeSelf && !isSwitching)
        {
            secondary.GetComponent<Animator>().SetTrigger("TakeOut");
            switchTimer = 0f;
            isSwitching = true;
        }

        if (isSwitching)
        {
            switchTimer += Time.deltaTime;
            if (switchTimer >= switchTime)
            {
                CompleteWeaponSwitch();
            }
        }
    }

    private void CompleteWeaponSwitch()
    {
        if (primary[currGun.GetCurrGunActive()].activeSelf)
        {
            primary[currGun.GetCurrGunActive()].SetActive(false);
            secondary.SetActive(true);
            this.isPrimaryActive = false;
            this.isSecondaryActive = true;
        }
        else if (secondary.activeSelf)
        {
            secondary.SetActive(false);
            primary[currGun.GetCurrGunActive()].SetActive(true);
            this.isPrimaryActive = true;
            this.isSecondaryActive = false;
        }

        isSwitching = false; // Reset the switching flag
        switchTimer = 0f; // Reset the timer
    }

    /*public void SwitchWeapon()
  {
      if (Input.GetKeyDown(KeyCode.Alpha2) && isPrimaryActive && !isSecondaryActive)
      {
          primary.GetComponent<Animator>().SetTrigger("TakeOut");
          // put timer here in if statement
          primary.SetActive(false);
          secondary.SetActive(true);
          this.isPrimaryActive = false;
          this.isSecondaryActive = true;
      }

      if(Input.GetKeyDown(KeyCode.Alpha1) && !isPrimaryActive && isSecondaryActive)
      {
          secondary.GetComponent<Animator>().SetTrigger("TakeOut");
          secondary.SetActive(false);
          primary.SetActive(true);
          this.isPrimaryActive = false;
          this.isSecondaryActive = true;

      }
  }*/
}
