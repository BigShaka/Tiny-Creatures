using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteractionUI : MonoBehaviour
{
    public GameObject interactionText; // Referencia al texto de la UI

    private void Start()
    {
        HideInteractionUI();
    }

    public void ShowInteractionUI()
    {
        interactionText.gameObject.SetActive(true);
    }

    public void HideInteractionUI()
    {
        interactionText.gameObject.SetActive(false);
    }
}
