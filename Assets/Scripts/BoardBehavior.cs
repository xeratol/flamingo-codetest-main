using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardBehavior : MonoBehaviour
{
    [SerializeField]
    private Transform _tileStart;

    [SerializeField]
    private Transform _tileEmpty;

    [SerializeField]
    private Transform _tileFlagQuiz;

    [SerializeField]
    private Transform _tileTextQuiz;

    [SerializeField]
    private TextAsset _levelDesignFile;

    private readonly List<TileBehavior> _tiles = new();

    private void Awake()
    {
        if (_tileStart == null)
            throw new ArgumentNullException($"Tile Start is not set");

        if (_tileEmpty == null)
            throw new ArgumentNullException($"Tile Empty is not set");

        if (_tileFlagQuiz == null)
            throw new ArgumentNullException($"Tile Flag Quiz is not set");

        if (_tileTextQuiz == null)
            throw new ArgumentNullException($"Tile Text Quiz is not set");

        if (_levelDesignFile == null)
            throw new ArgumentNullException($"Level Design File is not set");

    }

    private void Start()
    {
        var boardData = JsonUtility.FromJson<BoardData>(_levelDesignFile.text);

        foreach (var tile in boardData.Tiles)
        {
            var position = new Vector3(tile.PosX, tile.PosY, tile.PosZ);
            var newTile = tile.Type switch
            {
                TileType.Empty => Instantiate(_tiles.Count == 0 ? _tileStart : _tileEmpty, position, Quaternion.identity),
                TileType.TextQuiz => Instantiate(_tileTextQuiz, position, Quaternion.identity),
                TileType.FlagsQuiz => Instantiate(_tileFlagQuiz, position, Quaternion.identity),
                _ => throw new NotImplementedException(),
            };
            newTile.SetParent(transform);

            _tiles.Add(newTile.GetComponent<TileBehavior>());
        }
    }

    public IEnumerable<TileBehavior> GetTiles(int fromIndex, int steps)
    {
        if (fromIndex < 0)
            throw new ArgumentOutOfRangeException($"{nameof(fromIndex)} may not be less than 0");

        if (steps <= 0)
        {
            Debug.LogError($"Number of steps is less than or equal to 0");
            return Array.Empty<TileBehavior>();
        }

        var destinations = new List<TileBehavior>();

        for (int i = 1; i <= steps; ++i)
        {
            var nextTileIndex = (fromIndex + i) % _tiles.Count;
            destinations.Add(_tiles[nextTileIndex]);
        }

        return destinations;
    }
}
