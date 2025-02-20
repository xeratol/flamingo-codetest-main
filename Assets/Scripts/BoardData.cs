using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BoardData
{
    public TileData[] Tiles;

    public BoardData(TileData[] tiles)
    {
        Tiles = tiles;
    }
}
