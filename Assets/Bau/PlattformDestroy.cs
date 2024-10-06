using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlattformDestroy : MonoBehaviour
{
    public GameObject player; // Referencia al objeto del jugador
    public float destructionDelay = 2f; // Tiempo en segundos antes de que la plataforma se destruya
    private bool isPlayerOnPlatform;
    private Coroutine destructionCoroutine;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            isPlayerOnPlatform = true;
            if (destructionCoroutine == null)
            {
                destructionCoroutine = StartCoroutine(DestroyPlatform());
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == player)
        {
            isPlayerOnPlatform = false;
            if (destructionCoroutine != null)
            {
                StopCoroutine(destructionCoroutine);
                destructionCoroutine = null;
            }
        }
    }

    private IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(destructionDelay);
        gameObject.SetActive(false); 
    }
}

