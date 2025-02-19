using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedNumberGenerator : MonoBehaviour, INumberGenerator
{
    [Tooltip("The fixed number to generate")]
    public int FixedValue;

    public event Action<int> OnGenerateEvent;

    public void Generate()
    {
        OnGenerateEvent?.Invoke(FixedValue);
    }
}
