using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    private int direction = 1; // 1 = right, -1 = left

    [Header("Health")]
    public int health = 3;

    void Update()
    {
        // Move left and right constantly
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            // Flip direction when hitting a wall trigger
            direction *= -1;
        }
        else if (other.CompareTag("Parry"))
        {
            direction *= -1;

        }
        else if (other.CompareTag("Fireball"))
        {
            // Take damage from fireball
            TakeDamage(1);
            Destroy(other.gameObject);
        }
        
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
