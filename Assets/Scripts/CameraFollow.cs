using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float _snapDistance = 0.1f;

    [SerializeField]
    [Range(0f, 1f)]
    private float _followFactor = 0.1f;

    private Vector3 _offset;
    private Transform _target;

    private void Start()
    {
        // Assuming starting tile is at Vector3.zero
        _offset = transform.position;
    }

    private void Update()
    {
        if (_target == null)
            return;

        var targetPosition = _target.position + _offset;
        var distanceSq = (targetPosition - transform.position).sqrMagnitude;
        if (distanceSq > _snapDistance * _snapDistance)
        {
            var dir = targetPosition - transform.position;
            transform.Translate(dir * _followFactor);
        }
        else
        {
            transform.position = targetPosition;
        }
    }

    public void SetTarget(Transform target)
    {
        Debug.Log("Camera target set");
        _target = target;
    }
}
