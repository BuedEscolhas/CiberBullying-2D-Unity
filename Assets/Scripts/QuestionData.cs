using UnityEngine;

/// <summary>
/// ScriptableObject com os dados de uma única pergunta do quiz.
/// Criar via Assets → Create → CiberBullying → QuestionData
/// </summary>
[CreateAssetMenu(fileName = "QuestionData", menuName = "CiberBullying/QuestionData")]
public class QuestionData : ScriptableObject
{
    [TextArea(2, 4)]
    public string questionText;

    public AnswerData[] answers;

    [Range(0, 3)]
    public int correctAnswerIndex;

    [TextArea(1, 2)]
    public string voiceOffText; // texto que a voz off diz antes da pergunta (opcional)
}

[System.Serializable]
public struct AnswerData
{
    [TextArea(1, 2)]
    public string text;
}
