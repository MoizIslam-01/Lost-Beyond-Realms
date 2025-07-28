using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int health = 3;
    private int direction = 1;
    

    void Update()
    {
        
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            direction *= -1;
        }
        else if (other.CompareTag("Fireball"))
        {
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
