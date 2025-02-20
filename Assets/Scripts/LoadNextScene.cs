using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    [SerializeField]
    private int _indexOfNextScene = 1;

    [SerializeField]
    private float _delay = 1.0f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(_indexOfNextScene);
    }
}
