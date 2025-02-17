using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{
    public enum TileType
    {
        Empty,
        QuestionsQuiz,
        FlagsQuiz,
    }

    [SerializeField]
    private TileType _tileType;
}
