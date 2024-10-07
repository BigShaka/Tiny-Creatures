using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerData PlayerData = new();

    public Gliding gliding;
    public Laser laser;
    void Start()
    {
        SaveGameManager.LoadGame();

        PlayerData = SaveGameManager.CurrentSaveData.PlayerData;

        gliding.isGlidingUnlocked = PlayerData.glidingUnlocked;
        laser.isLaserUnlocked = PlayerData.laserUnlocked;
        gliding.gameObject.transform.position = PlayerData.lastPosition;
        gliding.gameObject.transform.rotation= PlayerData.lastRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
