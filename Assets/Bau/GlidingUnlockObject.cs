using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlidingUnlockObject : InteractableObject
{
    public Gliding glidingController; // Referencia al controlador del gliding

    public override void Interact()
    {
        HideInteractionUI();
        glidingController.UnlockGliding();
        Destroy(gameObject);
    }
}
