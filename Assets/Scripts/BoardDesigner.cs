using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class BoardDesigner : MonoBehaviour
{
    [SerializeField]
    private Transform _tileEmpty;

    [SerializeField]
    private Transform _tileFlagQuiz;

    [SerializeField]
    private Transform _tileTextQuiz;

    public void CreateEmptyTile()
    {
        CreateTile(_tileEmpty);
    }

    public void CreateFlagQuizTile()
    {
        CreateTile(_tileFlagQuiz);
    }

    public void CreateTextQuizTile()
    {
        CreateTile(_tileTextQuiz);
    }

    private void CreateTile(Transform sourceTile)
    {
        var tile = Instantiate(sourceTile, Vector3.zero, Quaternion.identity);
        tile.parent = transform;
    }

    public string Export()
    {
        var tiles = GetComponentsInChildren<TileBehavior>().ToList();
        var tileData = new List<TileData>();

        foreach (var tile in tiles)
        {
            var newTile = new TileData(tile.Type, tile.transform.position);
            tileData.Add(newTile);
        }

        var boardData = new BoardData(tileData.ToArray());
        var json = JsonUtility.ToJson(boardData);
        return json;
    }
}
