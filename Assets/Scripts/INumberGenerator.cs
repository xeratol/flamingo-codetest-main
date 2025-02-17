using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INumberGenerator
{
    public event Action<int> OnGenerate;

    public void Generate();
}
