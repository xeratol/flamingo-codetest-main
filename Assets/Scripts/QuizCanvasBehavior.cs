using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizCanvasBehavior : MonoBehaviour
{
    [SerializeField]
    private Image _avatar;

    [SerializeField]
    private TMP_Text _questionField;

    [SerializeField]
    private Image _imageHint;

    [SerializeField]
    private Image[] _choicesImage;

    [SerializeField]
    private TMP_Text[] _choicesText;

    public event Action<int> OnChoiceSelectedEvent;

    public string QuestionText
    {
        set { _questionField.text = value; }
    }

    public Sprite QuestionImage
    {
        set { _imageHint.sprite = value; }
    }

    private void Awake()
    {
        if (_avatar == null)
            throw new ArgumentNullException($"Avatar is not set");

        if (_questionField == null)
            throw new ArgumentNullException($"Question Field is not set");
    }

    public void SelectAnswer(int index)
    {
        Debug.Log($"Player selected {index}");
        OnChoiceSelectedEvent?.Invoke(index);
    }

    public void SetupChoices(Sprite[] choices)
    {
        if (_choicesImage == null || _choicesImage.Length == 0)
            throw new ArgumentNullException($"Image choices are not set");

        Debug.Assert(choices != null);
        Debug.Assert(choices.Length == _choicesImage.Length);

        for (var i = 0; i < choices.Length; i++)
        {
            _choicesImage[i].sprite = choices[i];
        }
    }

    public void SetupChoices(string[] choices)
    {
        if (_choicesText == null || _choicesText.Length == 0)
            throw new ArgumentNullException($"Text choices are not set");

        Debug.Assert(choices != null);
        Debug.Assert(choices.Length == _choicesText.Length);

        for (var i = 0; i < choices.Length; i++)
        {
            _choicesText[i].text = choices[i];
        }
    }
}
