using UnityEngine;

/// <summary>
/// Quando o player entra no trigger do buraco:
/// ativa o pinguim e toca a voz off.
/// </summary>
public class HoleTrigger : MonoBehaviour
{
    [SerializeField] private GameObject penguin;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        penguin?.SetActive(true);
    //  AudioManager.Instance?.PlayVoiceHole();
    }
}
