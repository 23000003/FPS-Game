using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI
{

    private readonly Slider healthSlider;

    public HealthBarUI(Slider slider)
    {
        healthSlider = slider;
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
