using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public int maxHealth = 100;
    public int currentHealth;
    public GameObject gameOverScreen;
    public GameObject healthBarObject; // Reference to the HealthBar GameObject

    public HealthBar healthBar;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances of Player!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Disable the PlaneSpawn and GunFire scripts
        PlaneSpawn.Instance.enabled = false;
        GunFire.Instance.enabled = false;

        // Hide the health bar
        healthBarObject.SetActive(false);

        // Show the game over screen
        gameOverScreen.SetActive(true);
    }
}
