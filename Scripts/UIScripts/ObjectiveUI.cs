using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// WAS PickUpUI
public class ObjectiveUI
{
    public static ObjectiveUI Instance { get; set; }

    private readonly TMP_Text interactionText;
    private readonly TMP_Text keyUIText;
    private readonly TMP_Text circlePointUIText;
    private readonly TMP_Text crateUIText;

    public ObjectiveUI(TMP_Text interactionText, TMP_Text keyUIText, TMP_Text circlePointUIText, TMP_Text crateUIText)
    {
        this.interactionText = interactionText;
        this.keyUIText = keyUIText;
        this.circlePointUIText = circlePointUIText;
        this.crateUIText = crateUIText;
    }

    public void EnableInteractionText(string text)
    {
        interactionText.text = text + " (F)";
        interactionText.gameObject.SetActive(true);

    }

    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }

    // ADD THE CRATE, CIRCLEPOINT HERE
    public void UpdatePickUpKeyText(string text)
    {
        keyUIText.text = " : " + text + " / " + GameState.Instance.GetTotalKeys().ToString();
    }

    public void UpdatePickUpCirclePointText(string text)
    {
        circlePointUIText.text = " : " + text + " / " + GameState.Instance.GetTotalCirclePoint().ToString();
    }

    public void UpdatePickUpCrateText(string text)
    {
        crateUIText.text = text + " / 1";
    }
}
