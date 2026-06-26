using UnityEngine;

public class BombObstacle : MonoBehaviour
{
    [SerializeField] private GameObject bombVisual;
    [SerializeField] private Collider2D blockCollider;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private TimerManager timerManager;

    private bool _defused;

    public void DefuseBomb()
    {
        if (_defused) return;
        _defused = true;
        timerManager?.StopTimer();
        if (blockCollider != null) blockCollider.enabled = false;
        if (bombVisual != null) bombVisual.SetActive(false);
    }

    /// <summary>
    /// Explode: toca efeito, repõe timer para nova tentativa, chama GameManager.
    /// Corrige bug original onde StartTimer() era chamado sem reset do currentTime.
    /// </summary>
    public void Explode()
    {
        if (_defused) return;

        if (explosionEffect != null)
        {
            explosionEffect.transform.position = bombVisual.transform.position;
            explosionEffect.Play();
        }

     /*   AudioManager.Instance?.PlayExplosion();
        timerManager?.ResetTimer();
        GameManager.Instance?.PlayerDied(); */
    }
}
