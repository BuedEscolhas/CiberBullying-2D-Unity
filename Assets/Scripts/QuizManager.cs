using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [Header("Quiz Panels")]
    public GameObject PenguinQuiz;
    public GameObject DynamiteQuiz;
    public GameObject WallQuiz;
    public GameObject UnicornQuiz;

    [Header("Obstacles")]
    public PenguinBridge penguinBridge;
    public BombObstacle bombObstacle;
    public TimerManager timerManager;

    private QuizType currentQuiz;

    public void OpenQuiz(QuizType type)
    {
        CloseAllQuizzes();

        currentQuiz = type;

        switch (type)
        {
            case QuizType.Penguin:
                PenguinQuiz.SetActive(true);
                break;

            case QuizType.Bomb:
                DynamiteQuiz.SetActive(true);
                break;

            case QuizType.Wall:
                WallQuiz.SetActive(true);
                break;

            case QuizType.Unicorn:
                UnicornQuiz.SetActive(true);
                break;
        }
    }

    void CloseAllQuizzes()
    {
        if (PenguinQuiz != null)
            PenguinQuiz.SetActive(false);

        if (DynamiteQuiz != null)
            DynamiteQuiz.SetActive(false);

        if (WallQuiz != null)
            WallQuiz.SetActive(false);

        if (UnicornQuiz != null)
            UnicornQuiz.SetActive(false);
    }

    public void CorrectAnswer()
    {
        CloseAllQuizzes();

        switch (currentQuiz)
        {
            case QuizType.Penguin:
                if (penguinBridge != null)
                    penguinBridge.StartBridge();
                break;

            case QuizType.Bomb:
                if (bombObstacle != null)
                    bombObstacle.DefuseBomb();
                break;

            case QuizType.Wall:
                Debug.Log("Parede removida");
                break;

            case QuizType.Unicorn:
                Debug.Log("Unicórnio libertado");
                break;
        }
    }

    public void WrongAnswer()
    {
        CloseAllQuizzes();

        switch (currentQuiz)
        {
            case QuizType.Bomb:
                if (bombObstacle != null)
                    timerManager.RemoveTime(30f);
                break;
        }
    }
}