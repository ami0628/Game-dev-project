
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform healthBar;

    [SerializeField] private float width = 127f;
    [SerializeField] private float height = 17f;

    public void SetMaxHealth(float maxHealth)
    {
        SetHealth(maxHealth, maxHealth);
    }

    public void SetHealth(float health, float maxHealth)
    {
        if (healthBar == null)
        {
            Debug.LogError(
                "HealthBar: Assign the health bar Fill RectTransform in the Inspector.",
                this
            );
            return;
        }

        if (maxHealth <= 0f)
        {
            Debug.LogError("MaxHealth must be greater than zero.", this);
            return;
        }

        float healthPercent = Mathf.Clamp01(health / maxHealth);

        healthBar.sizeDelta = new Vector2(
            width * healthPercent,
            height
        );
    }
}
