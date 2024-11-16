using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI gunAmmo;

    private void Update()
    {

        int bullets = WeaponController.Instance.GetBullets();
        int totalBullets = WeaponController.Instance.GetTotalbullets();

        if (WeaponSystem.Instance.GetWeaponSwitching().GetIsPrimary())
        {
            SetAmmoUIText(bullets + " / " + totalBullets);
        }
        else if (WeaponSystem.Instance.GetWeaponSwitching().GetIsSecondary())
        {
            SetAmmoUIText(bullets + " / " + totalBullets);
        }
    }


    private void SetAmmoUIText(string ammo)
    {
        gunAmmo.text = ammo;
    }



}

