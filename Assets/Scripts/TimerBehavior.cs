using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerBehavior : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    public event Action OnCountdownEndsEvent;

    public void StartCountdown(float duration)
    {
        StartCoroutine(StartCountdownInternal(duration));
    }

    private IEnumerator StartCountdownInternal(float duration)
    {
        var timeRemaining = duration;

        while (timeRemaining >= 0)
        {
            _text.text = timeRemaining.ToString("N0");
            yield return null;
            timeRemaining -= Time.deltaTime;
        }

        _text.text = "0";
        OnCountdownEndsEvent?.Invoke();
    }
}
