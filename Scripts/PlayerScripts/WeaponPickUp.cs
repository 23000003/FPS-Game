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
        if (Input.GetKeyDown(KeyCode.F) && PlayerInteraction.Instance.GetObjectTag() == "PickUpWeapons"
            && PlayerInteraction.Instance.GetInteractable().GetIndex() != currGunActive )
        {

            GameObject currGun = primary[currGunActive];
            currGun.SetActive(false);
            currGunActive = PlayerInteraction.Instance.GetInteractable().GetIndex();
            primary[currGunActive].SetActive(true);

        }
    }
}
