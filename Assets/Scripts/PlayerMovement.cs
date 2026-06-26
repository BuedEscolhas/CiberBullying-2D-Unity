using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _anim;
    private bool _isGrounded;
    private bool _jumpRequested;
    private bool _canMove = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    private void OnEnable() => InputManager.Instance?.OnJump.AddListener(RequestJump);
    private void OnDisable() => InputManager.Instance?.OnJump.RemoveListener(RequestJump);

    public void SetMovementEnabled(bool enabled) => _canMove = enabled;

    private void RequestJump()
    {
        if (_isGrounded && _canMove) _jumpRequested = true;
    }

    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float move = InputManager.Instance != null ? InputManager.Instance.HorizontalAxis : 0f;

        _anim.SetFloat("Speed", Mathf.Abs(move));
        _anim.SetBool("IsGrounded", _isGrounded);
        _anim.SetFloat("VerticalVelocity", _rb.linearVelocity.y);

        if (move > 0f) _sr.flipX = false;
        else if (move < 0f) _sr.flipX = true;
    }

    private void FixedUpdate()
    {
        if (!_canMove) return;

        float move = InputManager.Instance != null ? InputManager.Instance.HorizontalAxis : 0f;
        _rb.linearVelocity = new Vector2(move * speed, _rb.linearVelocity.y);

        if (_jumpRequested)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
            _jumpRequested = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}