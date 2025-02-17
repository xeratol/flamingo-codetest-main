using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    private PlayerBehavior[] _players;

    [SerializeField]
    private BoardBehavior _board;

    [SerializeField]
    private INumberGenerator _numberGenerator;

    private int _currentPlayer = 0;
    private bool _isAnyPlayerMoving = false;

    void Awake()
    {
        if (_players is null || _players.Count() == 0)
            throw new ArgumentException($"Players are not set");

        if (_board is null)
            throw new ArgumentException($"Board is not set");

        _numberGenerator = GetComponent<INumberGenerator>();
        if (_numberGenerator is null)
            throw new ArgumentException($"Number Generator is not set");

        foreach (var player in _players)
            player.OnMoveCompleteEvent += OnMoveCompleteEventHandler;

        _numberGenerator.OnGenerate += OnNumberGenerateEventHandler;
    }

    private void OnMoveCompleteEventHandler()
    {
        _isAnyPlayerMoving = false;
    }

    private void OnNumberGenerateEventHandler(int numSteps)
    {
        if (numSteps <= 0 || _isAnyPlayerMoving)
            return;

        _isAnyPlayerMoving = true;
        Debug.Log($"Moving player {_currentPlayer} by {numSteps}");

        var currentPlayer = _players[_currentPlayer];
        var currentTile = currentPlayer.CurrentTileIndex;
        var destinations = _board.GetTileDestinations(currentTile, numSteps);
        currentPlayer.MoveTo(destinations);

        _currentPlayer = (_currentPlayer + 1) % _players.Count();
    }
}
