using UnityEngine;
public class UnicornInteraction : MonoBehaviour
{
    [SerializeField] private QuizManager quiz;
    private bool _playerNear;
    private void OnEnable() => InputManager.Instance?.OnInteract.AddListener(TryInteract);
    private void OnDisable() => InputManager.Instance?.OnInteract.RemoveListener(TryInteract);
    private void TryInteract() { if (_playerNear) quiz?.OpenQuiz(QuizType.Unicorn); }
    private void OnTriggerEnter2D(Collider2D o) { if (o.CompareTag("Player")) _playerNear = true; }
    private void OnTriggerExit2D(Collider2D o) { if (o.CompareTag("Player")) _playerNear = false; } // BUG CORRIGIDO
}
