using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp
{
    private readonly GameObject[] primary;
    private int currGunActive = 0; // current primary gun holding

    public WeaponPickUp(GameObject[] primary)
    {
        this.primary = primary;
    }

    public int GetCurrGunActive() { return currGunActive; }

    public void PickUpWeapon()
    {
        if (Input.GetKeyDown(KeyCode.F) && Player.Instance.GetPlayerInteraction().GetObjectTag() == "PrimaryWeapons")
        {
            // Player.Instance.GetPlayerInteraction().GetInteractable().GetIndex() != currGunActive
            if (Player.Instance.GetPlayerInteraction().GetInteractable() is InteractGameObjects i)
            {
                if(i.GetIndex() != currGunActive)
                {
                    GameObject currGun = primary[currGunActive];
                    currGun.SetActive(false);
                    currGunActive = i.GetIndex();
                    primary[currGunActive].SetActive(true);
                }
            }
        }
    }
}
