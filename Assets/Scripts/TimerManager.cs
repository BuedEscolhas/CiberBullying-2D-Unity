using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startTime = 120f;

    [Header("UI")]
    public TMP_Text timerText;

    [Header("Bomb")]
    public BombObstacle bombObstacle;

    private float currentTime;
    private bool timerRunning = false;

    void Start()
    {
        currentTime = startTime;

        UpdateTimerUI();
    }

    void Update()
    {
        if (!timerRunning)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            timerRunning = false;

            if (bombObstacle != null)
            {
                bombObstacle.Explode();
            }
        }

        UpdateTimerUI();
    }

    public void StartTimer()
    {
        timerRunning = true;

        Debug.Log("Timer iniciado");
    }

    public void StopTimer()
    {
        timerRunning = false;

        Debug.Log("Timer parado");
    }

    public void RemoveTime(float amount)
    {
        currentTime -= amount;

        if (currentTime < 0)
            currentTime = 0;

        UpdateTimerUI();

        Debug.Log("Perdeu " + amount + " segundos");
    }

    void UpdateTimerUI()
    {
        if (timerText == null)
            return;

        timerText.text =
            Mathf.CeilToInt(currentTime).ToString();
    }
}