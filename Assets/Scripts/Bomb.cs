using UnityEngine;

public class BombObstacle : MonoBehaviour
{
    [Header("Bomb Objects")]
    public GameObject bombVisual;
    public Collider2D blockCollider;

    [Header("Explosion")]
    public ParticleSystem explosionEffect;

    [Header("Timer")]
    public TimerManager timerManager;

    public void DefuseBomb()
    {
        // Para o timer
        if (timerManager != null)
        {
            timerManager.StopTimer();
        }

        // Libera passagem
        if (blockCollider != null)
        {
            blockCollider.enabled = false;
        }

        // Remove a bomba
        if (bombVisual != null)
        {
            bombVisual.SetActive(false);
        }

        Debug.Log("Bomba desarmada!");
    }

    public void Explode()
    {
        Debug.Log("Tempo esgotado!");

        // Toca explosão
        if (explosionEffect != null)
        {
            explosionEffect.transform.position =
                bombVisual.transform.position;

            explosionEffect.Play();
        }

        // Reinicia o timer
        if (timerManager != null)
        {
            timerManager.StartTimer();
        }

        // NÃO remove a bomba
        // NÃO remove o collider
        // jogador deve tentar novamente
    }
}