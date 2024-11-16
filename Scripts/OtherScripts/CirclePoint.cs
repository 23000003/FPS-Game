using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePoint : MonoBehaviour
{
    ProgressBarUI progressBar;
    GameObject mainCircle;
    GameObject progressUI;

    private void Awake()
    {
        progressBar = GameObject.FindGameObjectWithTag("UISystem").GetComponent<ProgressBarUI>();
        mainCircle = GameObject.FindGameObjectWithTag("CircleParentCrate");
        progressUI = GameObject.FindGameObjectWithTag("ProgressBar");
        DisableProgressBar();
    }

    private void Update()
    {
        if(progressBar.getIsFinished())
        {
            DisableProgressBar();
            mainCircle.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("TEST");
        if (other.CompareTag("Player"))
        {
            print("TEST2");
            EnableProgressBar();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("TEST1");
        if (other.CompareTag("Player"))
        {
            print("TEST3");
            DisableProgressBar();
        }
    }

    private void DisableProgressBar() {
        progressBar.enabled = false;
        progressBar.setIsActive(false);
        //progressBar.gameObject.SetActive(false);
        progressUI.SetActive(false);
    }

    private void EnableProgressBar() {
        //progressBar.gameObject.SetActive(true);
        progressUI.SetActive(true);
        progressBar.enabled = true;
        progressBar.setIsActive(true); 
    }
}
