using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] private string message;
    [SerializeField] private UnityEvent onInteraction;
    [SerializeField] private int index;
    Outline outline;

    public string getMessage()
    {
        return this.message;
    }

    public int GetIndex()
    {
        return index;
    }

    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        onInteraction.Invoke();
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }

}
