using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto tiene el componente InteractableObject o cualquier componente que herede de InteractableObject
        InteractableObject interactable = other.GetComponent<InteractableObject>();
        if (interactable != null)
        {
            interactable.Interact();
        }
    }
}
