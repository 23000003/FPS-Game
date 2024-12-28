using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CirclePoint : MonoBehaviour, ICirclePoint
{
    public int index;

    private void Update()
    {
        if (UISystem.Instance.GetProgressBarUI().GetIsFinished() && UISystem.Instance.GetProgressBarUI().GetCircleTag() == "CirclePoint")
        {
            DisableProgressBar();
            UISystem.Instance.GetProgressBarUI().SetIsFinished(false);
            GameState.Instance.SetCirclePointCaptured(GameState.Instance.GetCirclePointCaptured() + 1);

            if(GameState.Instance.GetCirclePointCaptured() == GameState.Instance.GetTotalCirclePoint())
            {
                GameObject.FindGameObjectWithTag("CrateBlocker").SetActive(false);
            }

            UISystem.Instance.GetObjectiveUI().UpdatePickUpCirclePointText(GameState.Instance.GetCirclePointCaptured().ToString());
            GameState.Instance.EnableDisableCirclePoint(index, false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnableProgressBar();

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisableProgressBar();
        }
    }

    public void DisableProgressBar()
    {
        //ProgressBarUI.Instance.enabled = false;
        UISystem.Instance.GetProgressBarUI().SetCircleTag("");
        //progressBar.gameObject.SetActive(false);
        GameState.Instance.GetProgressUI().SetActive(false);

    }

    public void EnableProgressBar()
    {
        //progressBar.gameObject.SetActive(true);
        GameState.Instance.GetProgressUI().SetActive(true);
        //ProgressBarUI.Instance.enabled = true;
        UISystem.Instance.GetProgressBarUI().SetCircleTag("CirclePoint");
    }

}
