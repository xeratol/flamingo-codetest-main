using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{
    [SerializeField]
    private TileType _tileType;

    public TileType Type => _tileType;
}
