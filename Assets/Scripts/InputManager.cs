using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Centraliza todo o input: teclado (PC) e touch (tablet/WebGL).
/// Outros scripts subscrevem aos eventos — nenhum lê Input diretamente.
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public float HorizontalAxis { get; private set; }

    public UnityEvent OnJump = new();
    public UnityEvent OnInteract = new();

    // Referências aos botões de touch (arrastar no Inspector)
    [Header("Botões Touch (tablet)")]
    [SerializeField] private UnityEngine.UI.Button jumpButton;
    [SerializeField] private UnityEngine.UI.Button interactButton;

    private bool _touchLeft;
    private bool _touchRight;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        jumpButton?.onClick.AddListener(() => OnJump.Invoke());
        interactButton?.onClick.AddListener(() => OnInteract.Invoke());
    }

    // Chamados pelo EventTrigger dos botões de movimento (PointerDown / PointerUp)
    public void OnLeftDown() => _touchLeft = true;
    public void OnLeftUp() => _touchLeft = false;
    public void OnRightDown() => _touchRight = true;
    public void OnRightUp() => _touchRight = false;

    private void Update()
    {
        float keyboard = Input.GetAxisRaw("Horizontal");
        float touch = _touchRight ? 1f : (_touchLeft ? -1f : 0f);
        HorizontalAxis = keyboard != 0f ? keyboard : touch;

        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.W))
            OnJump.Invoke();

        if (Input.GetKeyDown(KeyCode.E))
            OnInteract.Invoke();
    }
}