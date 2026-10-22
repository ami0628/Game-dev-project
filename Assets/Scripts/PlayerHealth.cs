// Assigns the healthbar to player so it works in accordance to their life. 
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private HealthBar healthBar;

    private PlayerO2 playerScript;

    public float health { get; private set; }
    public float MaxHealth => maxHealth;

    private void Start()
    {
        playerScript = GetComponent<PlayerO2>();
        health = maxHealth;

        if (healthBar == null)
        {
            Debug.LogError(
                "PlayerHealth: Assign the HealthBar component in the Inspector.",
                this
            );
            return;
        }

        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        // player death 
        if (health < 1) 
        {
            // reset health for respawn
            health = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            // set true so playerScript runs death method
            playerScript.wasKilled = true;

        }

    }


    private void OnTriggerEnter(Collider other)
    {
        // deal damage to player when hit by enemy
        if (other.gameObject == GameObject.FindGameObjectWithTag("Enemy")) 
        {
            health -= 20f;
            healthBar.SetHealth(health, maxHealth);

        }
    }
}
