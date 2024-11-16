using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    public static ProgressBarUI Instance { get; set; }

    private bool isActive = false;
    private bool isFinished = false;

    private float duration = 10f;
    private float currentTime = 0f;

    public Slider slider;

    public void setIsActive(bool isActive) { this.isActive = isActive; }
    public bool getIsActive() { return isActive; }
    public bool getIsFinished() {  return isFinished; }

    void Update()
    {
        if (isActive)
        {
            UpdateProgress();
        }
    }

    private void UpdateProgress()
    {
        currentTime += Time.deltaTime;
        float progress = Mathf.Clamp01(currentTime / duration);
        slider.value = progress;

        // Optionally, deactivate when done
        if (currentTime >= duration)
        {
            isActive = false;
            this.isFinished = true;
        }
    }
}
