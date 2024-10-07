using UnityEngine;


[System.Serializable]
public class SaveData
{
    public PlayerData PlayerData;
}

[System.Serializable]
public struct PlayerData
{
    public int cosas;
    public Vector3 lastPosition;
    public Quaternion lastRotation;
}

