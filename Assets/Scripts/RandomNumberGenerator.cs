using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNumberGenerator : MonoBehaviour, INumberGenerator
{
    public int MinInclusive = 1;
    public int MaxExclusive = 7;

    public event System.Action<int> OnGenerate;

    public void Generate()
    {
        OnGenerate.Invoke(Random.Range(MinInclusive, MaxExclusive));
    }
}
