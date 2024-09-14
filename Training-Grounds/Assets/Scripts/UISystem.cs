using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI gunAmmo;
    private GunTrigger getGun; //Encapsulation (accessing its private ammo)

    private void Start()
    {
        getGun = GameObject.FindGameObjectWithTag("Weapon").GetComponent<GunTrigger>();
    }

    private void Update()
    {
        SetAmmo(getGun.bullets.ToString() + " / " + getGun.totalbullets.ToString());
    }

    private void SetAmmo(string ammo)
    {
        gunAmmo.text = ammo;
    }

}
