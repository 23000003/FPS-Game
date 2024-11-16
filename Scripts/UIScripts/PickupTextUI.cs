using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupTextUI : MonoBehaviour
{
    public static PickupTextUI instance;
    private int keysPicked = 0;
    private int keysMax = 5;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] TMP_Text interactionText;
    [SerializeField] TMP_Text pickUpText;

    GameObject circleQuest;

    private void Start()
    {
        circleQuest = GameObject.FindGameObjectWithTag("CircleQuestParent");
        circleQuest.SetActive(false);
    }

    public void EnableInteractionText(string text, RaycastHit hit)
    {
        print(text);
        interactionText.text = text + " (F)";
        interactionText.gameObject.SetActive(true);
                                
        if (Input.GetKeyDown(KeyCode.F) && hit.collider.tag == "GoldKey")
        {
            keysPicked++;
            UpdatePickUpKeyText(keysPicked.ToString());
        }
    }

    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }

    public void UpdatePickUpKeyText(string text)
    {
        pickUpText.text = text + " / " + keysMax.ToString();
        if(keysPicked == 2)
        {
            circleQuest.SetActive(true);
        }
    }

}
