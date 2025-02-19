using System;
using UnityEngine;

[Serializable]
public class QuizData
{
    [Serializable]
    public struct AnswerStruct
    {
        public string ImageID;
        public string Text;
    }

    public string ID;
    public QuizType QuestionType;
    public string Question;
    public string CustomImageID;
    public AnswerStruct[] Answers;
    public int CorrectAnswerIndex;

    public static QuizData CreateFromJson(string json)
    {
        return JsonUtility.FromJson<QuizData>(json);
    }
}
