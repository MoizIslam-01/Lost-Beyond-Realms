using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
            // Reload current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
