using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Singleton que gere vidas, checkpoints e respawn.
/// Não usa DontDestroyOnLoad pois o jogo tem uma única cena.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Configuração")]
    [SerializeField] private int startLives = 2;
    [SerializeField] private float respawnDelay = 1.5f;

    [Header("Respawn Points (índice = checkpoint)")]
    [SerializeField] private Transform[] respawnPoints;

    // Eventos subscritos pela UI e outros sistemas
    public UnityEvent<int> OnLivesChanged = new();
    public UnityEvent OnGameOver = new();
    public UnityEvent OnPlayerDied = new();
    public UnityEvent OnPlayerRespawn = new();

    public int Lives { get; private set; }
    public int CurrentCheckpoint { get; private set; }
    public bool IsGameOver { get; private set; }

    private Transform _player;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        Lives = startLives;
        CurrentCheckpoint = 0;

        // Único FindWithTag aceitável — feito uma vez no Start
        var playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            _player = playerObj.transform;
            _playerMovement = playerObj.GetComponent<PlayerMovement>();
        }

        OnLivesChanged.Invoke(Lives);
    }

    /// <summary>Chamado por qualquer sistema quando o player deve morrer.</summary>
    public void PlayerDied()
    {
        if (IsGameOver) return;

        Lives--;
        OnLivesChanged.Invoke(Lives);
        OnPlayerDied.Invoke();

        if (Lives <= 0)
        {
            IsGameOver = true;
            OnGameOver.Invoke();
            return;
        }

        StartCoroutine(RespawnCoroutine());
    }

    /// <summary>Avança o checkpoint. Só avança para a frente, nunca para trás.</summary>
    public void SetCheckpoint(int index)
    {
        if (index > CurrentCheckpoint)
            CurrentCheckpoint = index;
    }

    private System.Collections.IEnumerator RespawnCoroutine()
    {
        _playerMovement?.SetMovementEnabled(false);
        if (_player != null) _player.gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(respawnDelay);

        if (_player != null && respawnPoints != null
            && CurrentCheckpoint < respawnPoints.Length)
        {
            _player.position = respawnPoints[CurrentCheckpoint].position;
            _player.gameObject.SetActive(true);
        }

        _playerMovement?.SetMovementEnabled(true);
        OnPlayerRespawn.Invoke();
    }
}