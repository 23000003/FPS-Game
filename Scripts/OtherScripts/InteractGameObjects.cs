using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractGameObjects : Interactable
{
    [SerializeField] private int index;
    [SerializeField] private string gameObjectType;

    public string GetGameObjectType() { return this.gameObjectType; }
    public int GetIndex() { return this.index; }

    public override void Interact()
    {
        GetOnInteraction().Invoke();
    }

    public override void DisableOutline()
    {
        GetOutline().enabled = false;
    }


    public override void EnableOutline()
    {
        GetOutline().enabled = true;
    }

}