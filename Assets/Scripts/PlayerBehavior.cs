using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public int CurrentTileIndex { get; private set; } = 0;

    public bool IsMoving { get; private set; }
    public event Action OnMoveCompleteEvent;

    [SerializeField]
    private float MoveDuration = 0.5f;

    public void MoveTo(IEnumerable<Vector3> destinations)
    {
        StartCoroutine(Move(destinations));
    }

    private IEnumerator Move(IEnumerable<Vector3> destinations)
    {
        foreach (var destination in destinations)
        {
            transform.DOMove(destination, MoveDuration);
            ++CurrentTileIndex;
            yield return new WaitForSeconds(MoveDuration);
        }

        OnMoveCompleteEvent.Invoke();
    }
}
