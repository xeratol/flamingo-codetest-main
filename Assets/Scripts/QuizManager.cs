using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField]
    private ChildSelector _uiSelector;

    [SerializeField]
    private QuizRepository _quizRepo;

    [SerializeField]
    private QuizCanvasBehavior _textQuizCanvas;

    [SerializeField]
    private QuizCanvasBehavior _flagQuizCanvas;

    [SerializeField]
    private QuizCanvasResultBehavior _textQuizCanvasResult;

    [SerializeField]
    private QuizCanvasResultBehavior _flagQuizCanvasResult;

    [SerializeField]
    private string _successText = "Well done!";

    [SerializeField]
    private string _failureText = "Better luck next time!";

    [SerializeField]
    private float _quizDuration = 10.0f;

    private QuizData _currentQuizData;

    private void Awake()
    {
        if (_uiSelector == null)
            throw new ArgumentNullException($"UI Selector is not set");

        if (_flagQuizCanvas == null)
            throw new ArgumentNullException($"Flag Quiz Canvas is not set");

        if (_textQuizCanvas == null)
            throw new ArgumentNullException($"Text Quiz Canvas is not set");

        if (_quizRepo == null)
            throw new ArgumentNullException($"Quiz Repository not set");

        _textQuizCanvas.OnChoiceSelectedEvent += OnChoiceSelectedEventHandler;
        _flagQuizCanvas.OnChoiceSelectedEvent += OnChoiceSelectedEventHandler;
    }

    private void OnChoiceSelectedEventHandler(int choice)
    {
        QuizCanvasResultBehavior quizCanvasResult;
        switch (_currentQuizData.QuestionType)
        {
            case QuizType.Text:
                quizCanvasResult = _textQuizCanvasResult;
                break;
            case QuizType.Image:
                quizCanvasResult = _flagQuizCanvasResult;
                break;
            default:
                throw new IndexOutOfRangeException($"No canvas result for quiz type");
        }

        var answer = _currentQuizData.Answers[_currentQuizData.CorrectAnswerIndex];
        quizCanvasResult.AnswerText = answer.Text;
        if (!string.IsNullOrEmpty(answer.ImageID) && _quizRepo.FlagsDict.ContainsKey(answer.ImageID))
            quizCanvasResult.AnswerImage = _quizRepo.FlagsDict[answer.ImageID];
        quizCanvasResult.Remark = (choice == _currentQuizData.CorrectAnswerIndex) ? _successText : _failureText;
        _uiSelector.Select(quizCanvasResult.gameObject.name);
    }

    public void StartQuiz(QuizType quizType)
    {
        var quizData = _quizRepo.Fetch(quizType);
        Setup(quizData);
    }

    private void Setup(QuizData quizData)
    {
        _currentQuizData = quizData;

        Debug.Log($"Selected quiz: {quizData.ID}");

        switch (quizData.QuestionType)
        {
            case QuizType.Text:
                _uiSelector.Select(_textQuizCanvas.gameObject.name);
                _textQuizCanvas.Setup(quizData.Question, _quizDuration);
                _textQuizCanvas.SetupChoices(GetTextChoices(quizData.Answers));
                break;
            case QuizType.Image:
                _uiSelector.Select(_flagQuizCanvas.gameObject.name);
                _flagQuizCanvas.Setup(quizData.Question, _quizDuration);
                _flagQuizCanvas.SetupChoices(GetImageChoices(quizData.Answers));
                break;
        }
    }

    private Sprite[] GetImageChoices(QuizData.AnswerStruct[] answers)
    {
        // TODO randomize answers. Need to map correct answer index with randomized positions.
        var answersList = new List<QuizData.AnswerStruct>(answers);
        var choices = answersList.Select(answer => _quizRepo.FlagsDict[answer.ImageID]);
        return choices.ToArray();
    }

    private string[] GetTextChoices(QuizData.AnswerStruct[] answers)
    {
        // TODO randomize answers. Need to map correct answer index with randomized positions.
        var answersList = new List<QuizData.AnswerStruct>(answers);
        var choices = answersList.Select(answer => answer.Text);
        return choices.ToArray();
    }
}
