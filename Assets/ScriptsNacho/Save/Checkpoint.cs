using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<P_Movement>())
        {
            SaveGameManager.CurrentSaveData.PlayerData.lastPosition = other.transform.position;
            SaveGameManager.CurrentSaveData.PlayerData.lastRotation = other.transform.rotation;
            SaveGameManager.SaveGame();

            print(SaveGameManager.CurrentSaveData.PlayerData);
        }
    }
}
