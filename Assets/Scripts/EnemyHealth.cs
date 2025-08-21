using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float deathDelay = 0.1f;
    [SerializeField] private UnityEvent onDamaged;
    [SerializeField] private UnityEvent onDied;

    private int current;
    public bool IsAlive => current > 0;

    void Awake()
    {
        current = Mathf.Max(1, maxHealth);
    }

    public void TakeDamage(int amount, Vector2 hitPoint, Vector2 hitNormal, GameObject source)
    {
        if (!IsAlive) return;

        current -= Mathf.Max(1, amount);
        onDamaged?.Invoke();

        if (current <= 0)
        {
            onDied?.Invoke();
            // Disable collisions immediately to prevent double-hits on death
            foreach (var col in GetComponentsInChildren<Collider2D>())
                col.enabled = false;

            // Optional: play animation / VFX here
            Destroy(gameObject, deathDelay);
        }
    }
}
