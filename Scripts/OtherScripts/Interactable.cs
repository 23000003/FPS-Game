using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private string message;
    [SerializeField] private UnityEvent onInteraction;
    private Outline outline;

    public string GetMessage() { return this.message; }
    public Outline GetOutline() { return outline; }
    public UnityEvent GetOnInteraction() { return onInteraction; }

    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public abstract void Interact();
    public abstract void DisableOutline();
    public abstract void EnableOutline();

}
