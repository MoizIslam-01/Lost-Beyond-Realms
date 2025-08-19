using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static System.Action<Vector3> OnAnyEnemyDeath;
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
            direction *= -1;
        }
        else if (other.CompareTag("Falling"))
        {

            TakeDamage(health);
            Debug.Log("Attack hit");
        }
        else if (other.CompareTag("FINAL"))
        {
            // Take damage from final attack
            TakeDamage(health);
            Destroy(other.gameObject);
            Debug.Log("Final attack hit enemy!");
        }
        else if (other.CompareTag("Parry"))
        {
            //moveSpeed += 5;
            direction *= -1;

            // Give energy to player
            PlayerMovement pm = other.GetComponentInParent<PlayerMovement>();
            if (pm != null)
            {
                pm.GetEnergy();
                pm.FinalAtk();
                Debug.Log("Energy gained from parry!");
            }

            // STOP here so we don't damage the player this frame
            return;
        }
        else if (other.CompareTag("Player"))
        {

            PlayerMovement ph = other.GetComponentInParent<PlayerMovement>();
            if (ph != null)
            {
                ph.TakeDmg();
            }
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
    void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return; // ignore scene unload
        OnAnyEnemyDeath?.Invoke(transform.position);
    }
}
