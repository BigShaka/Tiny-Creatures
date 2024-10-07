using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Gliding gliding = GetComponent<Gliding>();
        Laser laser = GetComponent<Laser>();
        if (other.GetComponent<P_Movement>())
        {
            if (gliding != null && laser != null)
            {
                SaveGameManager.CurrentSaveData.PlayerData.glidingUnlocked = gliding.isGlidingUnlocked;
                SaveGameManager.CurrentSaveData.PlayerData.laserUnlocked = laser.isLaserUnlocked;
            }

            SaveGameManager.CurrentSaveData.PlayerData.lastPosition = other.transform.position;
            SaveGameManager.CurrentSaveData.PlayerData.lastRotation = other.transform.rotation;
            SaveGameManager.SaveGame();

            print(SaveGameManager.CurrentSaveData.PlayerData);
        }
    }
}
