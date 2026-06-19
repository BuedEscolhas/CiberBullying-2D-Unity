using UnityEngine;

public class PenguinInteraction : MonoBehaviour
{
    private bool playerNear;

    public QuizManager quiz;

    void Update()
    {
        if (
            playerNear &&
            Input.GetKeyDown(KeyCode.E)
        )
        {
            quiz.OpenQuiz(QuizType.Penguin);
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
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}