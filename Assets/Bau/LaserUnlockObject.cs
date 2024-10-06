using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserUnlockObject : InteractableObject
{
    public Laser laserController; // Referencia al controlador del láser
    public InteractableObject Ints;
    public override void Interact()
    {
        Ints.HideInteractionUI();
        laserController.UnlockLaser();
        Destroy(gameObject);
    }
}