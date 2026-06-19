using UnityEngine;

public class WallInteraction : MonoBehaviour
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
            quiz.OpenQuiz(QuizType.Wall);
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