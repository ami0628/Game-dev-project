
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private HealthBar healthBar;

    [Header("Testing")]
    [SerializeField] private bool enableTestKeys = true;

    public float Health { get; private set; }
    public float MaxHealth => maxHealth;

    private void Start()
    {
        Health = maxHealth;

        if (healthBar == null)
        {
            Debug.LogError(
                "PlayerHealth: Assign the HealthBar component in the Inspector.",
                this
            );
            return;
        }

        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(Health, maxHealth);
    }

    private void Update()
    {
        if (!enableTestKeys)
            return;

        if (Input.GetKeyDown(KeyCode.F))
            ChangeHealth(-20f);

        if (Input.GetKeyDown(KeyCode.H))
            ChangeHealth(20f);
    }

    public void ChangeHealth(float amount)
    {
        Health = Mathf.Clamp(Health + amount, 0f, maxHealth);

        if (healthBar != null)
            healthBar.SetHealth(Health, maxHealth);

        Debug.Log($"Player health: {Health}/{maxHealth}");

        if (Health <= 0f)
        {
            Debug.Log("Game Over! The player's health has reached zero.");
            // You can connect your Game Over screen here later.
        }
    }
}
