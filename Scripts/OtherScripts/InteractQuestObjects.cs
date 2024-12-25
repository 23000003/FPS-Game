using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Assign on Keys Crate, and use object Quest type for interaction
// Use this in GameState script to reference what u picked up

public class InteractQuestObjects : Interactable
{
    [SerializeField] private string objectQuestType;

    public string GetObjectQuestType()
    {
        return objectQuestType;
    }

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
