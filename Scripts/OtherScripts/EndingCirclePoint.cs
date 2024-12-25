using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCirclePoint : MonoBehaviour, ICirclePoint
{
    public static EndingCirclePoint Instance { get; private set; }
    private bool isGameOver = false;

    public bool GetIsGameOver() { return isGameOver; }
    public void SetIsGameOver(bool isGameOver) { this.isGameOver = isGameOver; }

    private void Awake()
    {
        //DisableProgressBar();
        Instance = this;
    }

    private void Update()
    {
        if (UISystem.Instance.GetProgressBarUI().GetIsFinished() && UISystem.Instance.GetProgressBarUI().GetCircleTag() == "EndingCirclePoint")
        {
            DisableProgressBar();
            UISystem.Instance.GetProgressBarUI().SetIsFinished(false);
            //GameState.Instance.GetEndingCP().SetActive(false);
            SetIsGameOver(true);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("COLLIDEDENDING");
            EnableProgressBar();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("COLLIDEDENDING1");
            DisableProgressBar();
        }
    }

    public void DisableProgressBar()
    {
        UISystem.Instance.GetProgressBarUI().SetCircleTag("");
        //progressBar.gameObject.SetActive(false);
        GameState.Instance.GetProgressUI().SetActive(false);
    }

    public void EnableProgressBar()
    {
        //progressBar.gameObject.SetActive(true);
        GameState.Instance.GetProgressUI().SetActive(true);
        UISystem.Instance.GetProgressBarUI().SetCircleTag("EndingCirclePoint");
    }

}
