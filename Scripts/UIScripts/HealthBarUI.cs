using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public static HealthBarUI instance;

    public Slider healthSlider;

    private void Awake()
    {
        instance = this;
    }

    public void SetHealthBar(float amount)
    {
        healthSlider.value = amount;
    }

    public void SetHealthBarUI(float amount)
    {
        healthSlider.maxValue = amount;
        SetHealthBar(amount);
    }


}
