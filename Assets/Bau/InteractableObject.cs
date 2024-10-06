using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public InteractionUI interactionUI;
    public virtual void Interact()
    {

    }

    public void ShowInteractionUI()
    {
        interactionUI.ShowInteractionUI();
    }

    public void HideInteractionUI()
    {
        interactionUI.HideInteractionUI();
    }
}