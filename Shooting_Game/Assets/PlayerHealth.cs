using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }

    public int maxHealth = 3; // Set your desired maximum health here
    public int CurrentHealth { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CurrentHealth = maxHealth;
        }
        else
        {
            Debug.LogError("Multiple instances of PlayerHealth!");
        }
    }

    public void DecreaseHealth(int amount)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);

        // TODO: Update health bar UI here

        if (CurrentHealth <= 0)
        {
            // TODO: Handle player death here
        }
    }
}
