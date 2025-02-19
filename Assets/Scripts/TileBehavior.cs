using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{
    public enum TileType
    {
        Empty,
        TextQuiz,
        FlagsQuiz,
    }

    [SerializeField]
    private TileType _tileType;

    public TileType Type => _tileType;
}
