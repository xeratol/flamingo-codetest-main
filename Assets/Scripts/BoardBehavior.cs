using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardBehavior : MonoBehaviour
{
    private List<TileBehavior> _tiles;

    private void Start()
    {
        // Assuming that board behavior does not have a tile behavior
        // And that children with TileBehavior are all on the 1st level
        _tiles = GetComponentsInChildren<TileBehavior>().ToList();
        if (_tiles.Count == 0)
            throw new InvalidOperationException($"No tiles found");
    }

    public IEnumerable<Vector3> GetTileDestinations(int fromIndex, int steps)
    {
        if (fromIndex < 0)
            throw new ArgumentOutOfRangeException($"{nameof(fromIndex)} may not be less than 0");

        if (steps <= 0)
        {
            Debug.LogError($"Number of steps is less than or equal to 0");
            return Array.Empty<Vector3>();
        }

        var destinations = new List<Vector3>();

        for (int i = 1; i <= steps; ++i)
        {
            var nextTileIndex = (fromIndex + i) % _tiles.Count;
            destinations.Add(_tiles[nextTileIndex].transform.position);
        }

        return destinations;
    }
}
