using UnityEngine;
using UnityEngine.Events;
using TMPro;

/// <summary>
/// Gere abertura/fecho dos painéis, pausa e lógica de resposta.
/// Cada painel de quiz tem os seus botões a chamar CorrectAnswer() ou WrongAnswer().
/// </summary>
public class QuizManager : MonoBehaviour
{
    [Header("Painéis")]
    [SerializeField] private GameObject penguinQuizPanel;
    [SerializeField] private GameObject dynamiteQuizPanel;
    [SerializeField] private GameObject wallQuizPanel;
    [SerializeField] private GameObject unicornQuizPanel;

    [Header("Perguntas (ScriptableObjects)")]
    [SerializeField] private QuestionData questionPenguin;
    [SerializeField] private QuestionData questionBomb;
    [SerializeField] private QuestionData[] questionsWall;   // 4 perguntas da parede
    [SerializeField] private QuestionData questionBird;

    [Header("UI partilhada de pergunta (opcional — ver notas)")]
    [SerializeField] private TMP_Text questionTextField;
    [SerializeField] private TMP_Text[] answerTextFields;

    [Header("Obstáculos")]
    [SerializeField] private PenguinBridge penguinBridge;
    [SerializeField] private BombObstacle bombObstacle;
    [SerializeField] private TimerManager timerManager;
    //[SerializeField] private WallProgress wallProgress;
    //[SerializeField] private BirdFlight birdFlight;

    [Header("Player")]
    [SerializeField] private PlayerMovement playerMovement;

    public UnityEvent OnQuizOpened = new();
    public UnityEvent OnQuizClosed = new();

    private QuizType _currentQuiz;

    public void OpenQuiz(QuizType type)
    {
        _currentQuiz = type;
        CloseAllPanels();

        Time.timeScale = 0f;
        playerMovement?.SetMovementEnabled(false);

        switch (type)
        {
            case QuizType.Penguin: penguinQuizPanel?.SetActive(true); break;
            case QuizType.Bomb: dynamiteQuizPanel?.SetActive(true); break;
            case QuizType.Wall: wallQuizPanel?.SetActive(true); break;
            case QuizType.Unicorn: unicornQuizPanel?.SetActive(true); break;
        }

        OnQuizOpened.Invoke();
    }

    public void CorrectAnswer()
    {
    /*    AudioManager.Instance?.PlayCorrect();
        CloseQuiz(); */

        switch (_currentQuiz)
        {
            case QuizType.Penguin:
                penguinBridge?.StartBridge();
                GameManager.Instance?.SetCheckpoint(1);
                break;

            case QuizType.Bomb:
                bombObstacle?.DefuseBomb();
                GameManager.Instance?.SetCheckpoint(2);
                break;

        /*    case QuizType.Wall:
                wallProgress?.AdvanceStep();
                break; 

              case QuizType.Unicorn:
                birdFlight?.StartFlight();
                GameManager.Instance?.SetCheckpoint(4);
                break; */
        }
    }

    public void WrongAnswer()
    {
//      AudioManager.Instance?.PlayWrong();

        switch (_currentQuiz)
        {
            case QuizType.Bomb:
                CloseQuiz();
                timerManager?.RemoveTime(30f);
                break;

            case QuizType.Penguin:
            case QuizType.Unicorn:
                CloseQuiz();
                GameManager.Instance?.PlayerDied();
                break;

   //       case QuizType.Wall:
   //             CloseQuiz();
   //             wallProgress?.RegressStep();
   //             break;
        }
    }

    private void CloseQuiz()
    {
        CloseAllPanels();
        Time.timeScale = 1f;
        playerMovement?.SetMovementEnabled(true);
        OnQuizClosed.Invoke();
    }

    private void CloseAllPanels()
    {
        penguinQuizPanel?.SetActive(false);
        dynamiteQuizPanel?.SetActive(false);
        wallQuizPanel?.SetActive(false);
        unicornQuizPanel?.SetActive(false);
    }
}
