using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedNumberGenerator : MonoBehaviour, INumberGenerator
{
    [Tooltip("The fixed number to generate")]
    public int FixedValue;

    public event Action<int> OnGenerate;

    public void Generate()
    {
        OnGenerate.Invoke(FixedValue);
    }
}
