using UnityEngine;

public class UnicornInteraction : MonoBehaviour
{
    public QuizManager quiz;

    private bool playerNear;

    void Update()
    {
        if (
            playerNear &&
            Input.GetKeyDown(KeyCode.E)
        )
        {
            quiz.OpenQuiz(QuizType.Unicorn);
        }
    }

    private void OnTriggerEnter2D(
        Collider2D other
    )
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit2D(
        Collider2D other
    )
    {
        playerNear = false;
    }
}