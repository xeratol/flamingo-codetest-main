using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INumberGenerator
{
    public event Action<int> OnGenerateEvent;

    public void Generate();
}
