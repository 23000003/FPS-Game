using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerInteraction
{
    public static PlayerInteraction Instance { get; set; }

    private readonly float playerReach;
    private readonly GameObject cameraRayCast;
    private string objectTag;
    private Interactable currInteractable;
    private bool isPickedUp = false;

    public PlayerInteraction(float playerReach, GameObject cameraRayCast, string objectTag)
    {
        this.playerReach = playerReach;
        this.cameraRayCast = cameraRayCast;
        this.objectTag = objectTag;
        currInteractable = null;
    }

    public string GetObjectTag() { return objectTag; }
    public bool GetIsPickedUp() {  return isPickedUp; }
    public Interactable GetInteractable() { return currInteractable; }

    public void QuestObjectInteraction()
    {
        if (Input.GetKey(KeyCode.F) && currInteractable != null &&
            (objectTag == "Door" || objectTag == "Key" || objectTag == "Crate"))
        {
            currInteractable.Interact();

            if(objectTag == "Crate")
            {
                GameState.Instance.SetIsCrateDone(true);
                GameObject.FindGameObjectWithTag("CrateUI").SetActive(false);
                UISystem.Instance.GetObjectiveUI().UpdatePickUpCrateText("1");
            }

            if(objectTag == "Key")
            {
                GameState.Instance.SetGoldKeysPicked(GameState.Instance.GetKeysPicked() + 1);
                UISystem.Instance.GetObjectiveUI().UpdatePickUpKeyText(GameState.Instance.GetKeysPicked().ToString());
            }
        }
    }

    public void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(cameraRayCast.transform.position, cameraRayCast.transform.forward);
        if(Physics.Raycast(ray, out hit, playerReach))
        {
            //hit.collider.tag == "GoldKey" || hit.collider.tag == "DoorToCrate" ||
            //    hit.collider.tag == "QuestCrate" || hit.collider.tag == "AmmoBox" ||
            //    hit.collider.tag == "PickUpWeapons" || hit.collider.tag == "PickUpPistol"
            if (hit.collider.tag == "QuestObject" || hit.collider.tag == "GameObject")
            {
                Interactable newInteractable;

                if (hit.collider.tag == "QuestObject")
                {
                    newInteractable = hit.collider.GetComponent<InteractQuestObjects>();

                    if(newInteractable is InteractQuestObjects i)
                    {
                        objectTag = i.GetObjectQuestType();
                    }
                }
                else
                {
                    newInteractable = hit.collider.GetComponent<InteractGameObjects>();

                    if (newInteractable is InteractGameObjects i)
                    {
                        objectTag = i.GetGameObjectType();
                    }

                }

                isPickedUp = Input.GetKey(KeyCode.F);

                if(currInteractable && newInteractable != currInteractable)
                {
                    currInteractable.DisableOutline();
                }

                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else
                {
                    DisableCurrentInteractable();
                }
            }
            else
            {
                objectTag = "";
                isPickedUp = false;
                DisableCurrentInteractable();
            }
        }
        else
        {
            isPickedUp = false;
            DisableCurrentInteractable();
            objectTag = "";
        }
    }

    private void SetNewCurrentInteractable(Interactable newInteractable)
    {
        UISystem.Instance.GetObjectiveUI().EnableInteractionText(newInteractable.GetMessage());
        currInteractable = newInteractable;
        currInteractable.EnableOutline();
        
    }

    private void DisableCurrentInteractable()
    {
        UISystem.Instance.GetObjectiveUI().DisableInteractionText();
        if (currInteractable)
        {
            currInteractable.DisableOutline();
            currInteractable = null;
        }
    }
}
