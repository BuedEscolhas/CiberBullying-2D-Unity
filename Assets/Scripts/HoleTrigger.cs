using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    public GameObject penguin;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            penguin.SetActive(true);
        }
    }
}