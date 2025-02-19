using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizCanvasResultBehavior : MonoBehaviour
{
    [SerializeField]
    private Image _avatar;

    [SerializeField]
    private TMP_Text _remarkField;

    [SerializeField]
    private TMP_Text _answerField;

    [SerializeField]
    private Image _answerImage;

    private void Awake()
    {
        if (_remarkField == null)
            throw new ArgumentNullException($"Remark Field is not set");
    }

    public string Remark { set { _remarkField.text = value; } }
    public string AnswerText
    {
        set
        {
            if (_answerField != null)
                _answerField.text = value;
        }
    }
    public Sprite AnswerImage
    {
        set
        {
            if (_answerImage != null)
                _answerImage.sprite = value;
        }
    }
}
