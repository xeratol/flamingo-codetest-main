using System;
using UnityEngine;

[Serializable]
public class TileData
{
    public TileType Type;
    public float PosX;
    public float PosY;
    public float PosZ;

    public TileData(TileType type, Vector3 pos)
    {
        Type = type;
        PosX = pos.x;
        PosY = pos.y;
        PosZ = pos.z;
    }
}
