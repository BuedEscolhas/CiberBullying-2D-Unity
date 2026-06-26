using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HUD de vidas. Subscreve ao evento OnLivesChanged do GameManager.
/// </summary>
public class LivesUI : MonoBehaviour
{
    [SerializeField] private Image[] lifeIcons;
    [SerializeField] private Sprite lifeFullSprite;
    [SerializeField] private Sprite lifeEmptySprite;

    private void Start()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.OnLivesChanged.AddListener(UpdateUI);
        UpdateUI(GameManager.Instance.Lives);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnLivesChanged.RemoveListener(UpdateUI);
    }

    private void UpdateUI(int lives)
    {
        for (int i = 0; i < lifeIcons.Length; i++)
            lifeIcons[i].sprite = i < lives ? lifeFullSprite : lifeEmptySprite;
    }
}
