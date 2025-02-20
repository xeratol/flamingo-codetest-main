using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public int CurrentTileIndex { get; private set; } = 0;

    public event Action<TileBehavior> OnMoveCompleteEvent;

    [SerializeField]
    private float MoveDuration = 0.5f;

    [SerializeField]
    private Transform _fireworksOnPass;
    private Transform _fireworksOnPassInstance;

    [SerializeField]
    private Transform _fireworksOnLand;
    private Transform _fireworksOnLandInstance;

    [SerializeField]
    private Animator _animator;

    public void MoveTo(IEnumerable<TileBehavior> destinations)
    {
        StartCoroutine(Move(destinations));
    }

    private IEnumerator Move(IEnumerable<TileBehavior> destinations)
    {
        _animator.SetBool("Moving", true);
        foreach (var destination in destinations)
        {
            transform.DOMove(destination.transform.position, MoveDuration);

            yield return new WaitForSeconds(MoveDuration);

            ++CurrentTileIndex;
            if (destination.Type == TileBehavior.TileType.Empty)
            {
                if (destination == destinations.Last())
                    ShowParticlesOnLand();
                else
                    ShowParticlesOnPass();
            }
        }

        OnMoveCompleteEvent?.Invoke(destinations.Last());
        _animator.SetBool("Moving", false);
    }

    private void ShowParticlesOnPass()
    {
        if (_fireworksOnPass == null)
            return;

        if (_fireworksOnPassInstance == null)
            _fireworksOnPassInstance = Instantiate(_fireworksOnPass, transform.position, Quaternion.identity);
        else
        {
            _fireworksOnPassInstance.position = transform.position;
            _fireworksOnPassInstance.GetComponent<ParticleSystem>().Play();
        }
    }

    private void ShowParticlesOnLand()
    {
        if (_fireworksOnLand == null)
            return;

        if (_fireworksOnLandInstance == null)
            _fireworksOnLandInstance = Instantiate(_fireworksOnLand, transform.position, Quaternion.identity);
        else
        {
            _fireworksOnLandInstance.position = transform.position;
            _fireworksOnLandInstance.GetComponent<ParticleSystem>().Play();
        }
    }
}
