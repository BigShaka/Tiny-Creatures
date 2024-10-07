using UnityEngine;


[System.Serializable]
public class SaveData
{
    public PlayerData PlayerData;
}

[System.Serializable]
public struct PlayerData
{
    public bool glidingUnlocked;
    public bool laserUnlocked;
    public Vector3 lastPosition;
    public Quaternion lastRotation;
}

