using System;
using TMPro;
using UnityEngine;

public class ScorePopupBehavior : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    public string Text
    {
        set { _text.text = value; }
    }

    void Awake()
    {
        if (_text == null)
            throw new ArgumentNullException("Text not set");
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
