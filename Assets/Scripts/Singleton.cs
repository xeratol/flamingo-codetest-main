using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T>
    where T : class
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
                throw new NullReferenceException($"Instance of {typeof(T)} has not been set");

            return _instance;
        }
    }

    public static void Init(T instance)
    {
        if (_instance is not null)
            throw new InvalidOperationException($"Multiple instances of {typeof(T)} detected");

        _instance = instance;
    }
}
