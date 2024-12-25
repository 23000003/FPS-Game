using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI
{

    private readonly float duration = 10f;
    private float currentTime = 0f;
    private bool isFinished = false;
    private string circleTag = "";
    private Slider slider;

    public ProgressBarUI(Slider slider)
    {
        this.slider = slider;
    }


    public void SetCircleTag(string isActive) { this.circleTag = isActive; }
    public string GetCircleTag() { return circleTag; }
    public bool GetIsFinished() {  return isFinished; }
    public void SetIsFinished(bool isFinished) { this.isFinished = isFinished; }

    public void UpdateProgress()
    {

        if (circleTag == "CirclePoint" || circleTag == "EndingCirclePoint")
        {
            currentTime += Time.deltaTime;
            float progress = Mathf.Clamp01(currentTime / duration);
            slider.value = progress;

            if (currentTime >= duration)
            {
                this.isFinished = true; // deactivate the circle, the circle script will turn it back to false
            }
        }
        else
        {
            circleTag = "";
            currentTime = 0f;
            slider.value = currentTime;
        }

    }


}
