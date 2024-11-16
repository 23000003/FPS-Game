using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; set; }

    [SerializeField] private float playerReach = 3f;
    [SerializeField] private GameObject cameraRayCast;
    private Interactable currInteractable;

    private string objectTag = "";
    public string GetObjectTag() { return objectTag; }
    public Interactable GetInteractable() { return currInteractable; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        CheckInteraction();
        QuestObjectInteraction(); 
    }

    private void QuestObjectInteraction()
    {
        if (Input.GetKeyDown(KeyCode.F) && currInteractable != null &&
            (objectTag == "GoldKey" || objectTag == "DoorToCrate" || objectTag == "QuestCrate"))
        {
            currInteractable.Interact();
        }
    }

    private void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(cameraRayCast.transform.position, cameraRayCast.transform.forward);
        if(Physics.Raycast(ray, out hit, playerReach))
        {
            if(hit.collider.tag == "GoldKey" || hit.collider.tag == "DoorToCrate" ||
                hit.collider.tag == "QuestCrate" || hit.collider.tag == "AmmoBox" || 
                hit.collider.tag == "PickUpWeapons" || hit.collider.tag == "PickUpPistol")
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                objectTag = hit.collider.tag;

                if(currInteractable && newInteractable != currInteractable)
                {
                    currInteractable.DisableOutline();
                }

                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable, hit);
                }

                else
                {
                    DisableCurrentInteractable();
                }
            }
            else
            {
                objectTag = "";
                DisableCurrentInteractable();
            }
        }
        else
        {
            DisableCurrentInteractable();
            objectTag = "";
        }
    }

    private void SetNewCurrentInteractable(Interactable newInteractable, RaycastHit hit)
    {
        print(newInteractable.getMessage());
        PickupTextUI.instance.EnableInteractionText(newInteractable.getMessage(), hit);
        currInteractable = newInteractable;
        currInteractable.EnableOutline();
        
    }

    private void DisableCurrentInteractable()
    {
        PickupTextUI.instance.DisableInteractionText();
        if (currInteractable)
        {
            currInteractable.DisableOutline();
            currInteractable = null;
        }
    }
}
