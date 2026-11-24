// Assigns the healthbar to player so it works in accordance to their life. 
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private HealthBar healthBar;


    // vignette
    public Image damageVignette;
    private float maxAlphaValue = 0.75f;
    private float fadeIn = 8f;
    private float fadeOut = 2f;
    private bool isFadingIn = false;

    private PlayerDeath playerScript;

    public float health { get; private set; }
    public float MaxHealth => maxHealth;

    private void Start()
    {
        playerScript = GetComponent<PlayerDeath>();
        health = maxHealth;

        if (damageVignette != null) {SetVignetteAlpha(0f);}

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

        // manage vignette every frame

        if (damageVignette != null) 
        {
            float currentAlphaValue = damageVignette.color.a;

            if (isFadingIn)
            {
                // Move alpha towards maxAlpha
                currentAlphaValue = Mathf.MoveTowards(currentAlphaValue, maxAlphaValue, fadeIn * Time.deltaTime);
                SetVignetteAlpha(currentAlphaValue);

                // finish fading in when at max value
                if (currentAlphaValue >= maxAlphaValue) { isFadingIn = false; }
            }
            else
            {
                // Move alpha towards transparent
                currentAlphaValue = Mathf.MoveTowards(currentAlphaValue, 0f, fadeOut * Time.deltaTime);
                SetVignetteAlpha(currentAlphaValue);
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        // deal damage to player when hit by enemy
        if (GameObject.FindGameObjectsWithTag("Enemy").Contains(other.gameObject)) 
        {
            health -= 20f;
            healthBar.SetHealth(health, maxHealth);

            // apply red vignette when hit
            isFadingIn = true;
        }
    }

    private void SetVignetteAlpha(float alpha)
    {
        Color c = damageVignette.color;
        c.a = alpha;
        damageVignette.color = c;
    }
}
