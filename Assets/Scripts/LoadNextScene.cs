using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public int IndexOfNextScene = 1;

    void Start()
    {
        SceneManager.LoadScene(IndexOfNextScene);
    }
}
