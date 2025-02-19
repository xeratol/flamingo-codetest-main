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

    [SerializeField]
    private ChildSelector _uiSelector;

    [SerializeField]
    private QuizManager _quizManager;

    private int _currentPlayer = 0;
    private bool _isAnyPlayerMoving = false;

    void Awake()
    {
        if (_players == null || _players.Count() == 0)
            throw new ArgumentException($"Players are not set");

        if (_board == null)
            throw new ArgumentException($"Board is not set");

        _numberGenerator = GetComponent<INumberGenerator>();
        if (_numberGenerator == null)
            throw new ArgumentException($"Number Generator is not set");

        if (_uiSelector == null)
            throw new ArgumentException($"UI Selector is not set");

        if (_quizManager == null)
            throw new ArgumentException($"Quiz Manager is not set");

        foreach (var player in _players)
            player.OnMoveCompleteEvent += OnMoveCompleteEventHandler;

        _numberGenerator.OnGenerateEvent += OnNumberGenerateEventHandler;
    }

    public void MovePlayer()
    {
        _numberGenerator.Generate();
    }

    private void OnMoveCompleteEventHandler(TileBehavior finalTile)
    {
        _isAnyPlayerMoving = false;

        switch (finalTile.Type)
        {
            case TileBehavior.TileType.TextQuiz:
                _quizManager.StartQuiz(QuizType.Text);
                break;
            case TileBehavior.TileType.FlagsQuiz:
                _quizManager.StartQuiz(QuizType.Image);
                break;
        }
    }

    private void OnNumberGenerateEventHandler(int numSteps)
    {
        if (numSteps <= 0 || _isAnyPlayerMoving)
            return;

        _isAnyPlayerMoving = true;
        Debug.Log($"Moving player {_currentPlayer} by {numSteps}");

        var currentPlayer = _players[_currentPlayer];
        var currentTile = currentPlayer.CurrentTileIndex;
        var destinations = _board.GetTiles(currentTile, numSteps);
        currentPlayer.MoveTo(destinations);

        _currentPlayer = (_currentPlayer + 1) % _players.Count();
    }
}
