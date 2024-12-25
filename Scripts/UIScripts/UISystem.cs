using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    public static UISystem Instance { get; private set; }

    private ProgressBarUI progressBarUI;
    private HealthBarUI healthBarUI;
    private ObjectiveUI objUI;

    [SerializeField] private TextMeshProUGUI gunAmmo;

    //objUI
    [SerializeField] private TMP_Text circlePointUIText;
    [SerializeField] private TMP_Text interactionText;
    [SerializeField] private TMP_Text crateUIText;
    [SerializeField] private TMP_Text keyUIText;
    [SerializeField] private TMP_Text courseTimeText;
    [SerializeField] private TMP_Text totalKillsText;

    //progressBarUI
    [SerializeField] private Slider slider;

    public HealthBarUI GetHealthBarUI() { return healthBarUI; }
    public ObjectiveUI GetObjectiveUI() { return objUI; }
    public ProgressBarUI GetProgressBarUI() { return progressBarUI; }

    private void Awake()
    {
        Instance = this;
        healthBarUI = new HealthBarUI(GameObject.FindGameObjectWithTag("HealthBarSlider").GetComponent<Slider>());
        progressBarUI = new ProgressBarUI(slider);
        objUI = new ObjectiveUI(interactionText, keyUIText, circlePointUIText, crateUIText);
    }

    private void Update()
    {

        try
        {
            SetAmmoUIText();
            progressBarUI.UpdateProgress();
        }
        catch (NullReferenceException err)
        {
            Debug.Log(err.ToString());
        }
    }


    private void SetAmmoUIText()
    {
        int bullets = WeaponController.Instance.GetBullets();
        int totalBullets = WeaponController.Instance.GetTotalbullets();       

        gunAmmo.text = bullets.ToString() + " / " + totalBullets.ToString();
    }


    public void GameOverDataUI(string courseTime)
    {
        if ( courseTimeText != null && totalKillsText != null )
        {
            courseTimeText.text = courseTime;

            totalKillsText.text = Player.Instance.GetStats().GetKills().ToString();

        }
    }

}

