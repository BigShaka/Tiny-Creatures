using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interction : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E; // Tecla para interactuar
    public float interactionRange = 2f; // Rango de interacción
    public LayerMask interactableLayer; // Capa de objetos interactuables

    private bool canInteract = false;
    private InteractableObject currentInteractable;

    private void Update()
    {
        if (Input.GetKeyDown(interactKey) && canInteract)
        {
            currentInteractable.Interact();
        }

        CheckForInteractables();
    }

    private void CheckForInteractables()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange, interactableLayer);
        if (hitColliders.Length > 0)
        {
            currentInteractable = hitColliders[0].GetComponent<InteractableObject>();
            if (currentInteractable != null)
            {
                canInteract = true;
                currentInteractable.ShowInteractionUI();
            }
        }
        else
        {
            canInteract = false;
            if (currentInteractable != null)
            {
                currentInteractable.HideInteractionUI();
                currentInteractable = null;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
