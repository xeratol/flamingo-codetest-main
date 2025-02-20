using System;
using System.Collections.Generic;
using UnityEngine;

public class QuizRepository : MonoBehaviour
{
    [SerializeField]
    private TextAsset[] _quizzes;
    private readonly List<QuizData> _textQuizzes = new();
    private readonly List<QuizData> _flagQuizzes = new();

    [Serializable]
    private struct FlagKeyPair
    {
        public string Code;
        public Sprite Image;
    }

    [SerializeField]
    private FlagKeyPair[] _flagPairs;
    private readonly Dictionary<string, Sprite> _flagsDict = new();
    public IReadOnlyDictionary<string, Sprite> FlagsDict => _flagsDict;

    private void Awake()
    {
        // Ideally, we don't store quizzes in memory.
        // Rather, we fetch from a server when needed.
        foreach (var quiz in _quizzes)
        {
            var quizData = QuizData.CreateFromJson(quiz.text);
            switch (quizData.QuestionType)
            {
                case QuizType.Text:
                    _textQuizzes.Add(quizData);
                    break;
                case QuizType.Image:
                    _flagQuizzes.Add(quizData);
                    break;
            }
        }

        foreach (var flagPair in _flagPairs)
        {
            if (_flagsDict.ContainsKey(flagPair.Code))
            {
                Debug.LogError($"Flags Dictionary already contains ({flagPair.Code})");
                continue;
            }
            _flagsDict[flagPair.Code] = flagPair.Image;
        }
    }

    public QuizData Fetch(QuizType quizType)
    {
        List<QuizData> sourceList = quizType switch
        {
            QuizType.Text => _textQuizzes,
            QuizType.Image => _flagQuizzes,
            _ => throw new IndexOutOfRangeException($"Unsupported quiz type ({quizType})"),
        };

        var index = UnityEngine.Random.Range(0, sourceList.Count);
        return sourceList[index];
    }

}
