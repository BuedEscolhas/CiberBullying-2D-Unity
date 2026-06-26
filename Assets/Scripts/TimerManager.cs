using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float startTime = 60f;   // ← requisito: 60s
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private BombObstacle bombObstacle;

    public float CurrentTime { get; private set; }
    public bool IsRunning { get; private set; }

    private float _uiTimer;
    private const float UI_INTERVAL = 0.1f;

    private void Start() => ResetTimer();

    private void Update()
    {
        if (!IsRunning) return;

        CurrentTime -= Time.deltaTime;

        _uiTimer += Time.deltaTime;
        if (_uiTimer >= UI_INTERVAL) { _uiTimer = 0f; UpdateUI(); }

        if (CurrentTime <= 0f)
        {
            CurrentTime = 0f;
            IsRunning = false;
            UpdateUI();
            bombObstacle?.Explode();
        }
    }

    public void StartTimer() => IsRunning = true;
    public void StopTimer() => IsRunning = false;

    /// <summary>Repõe para startTime e para o timer. Corrige bug original.</summary>
    public void ResetTimer()
    {
        CurrentTime = startTime;
        IsRunning = false;
        UpdateUI();
    }

    /// <summary>Penalidade de resposta errada na bomba (-30s).</summary>
    public void RemoveTime(float amount)
    {
        CurrentTime = Mathf.Max(0f, CurrentTime - amount);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (timerText != null)
            timerText.text = Mathf.CeilToInt(CurrentTime).ToString();
    }
}
