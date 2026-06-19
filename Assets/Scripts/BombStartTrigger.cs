using UnityEngine;

public class BombStartTrigger : MonoBehaviour
{
    public TimerManager timerManager;

    private bool started = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (
            !started &&
            other.CompareTag("Player")
        )
        {
            started = true;

            timerManager.StartTimer();
        }
    }
}