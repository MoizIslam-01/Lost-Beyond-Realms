using UnityEngine;

public interface IDamageable
{
    bool IsAlive { get; }
    void TakeDamage(int amount, Vector2 hitPoint, Vector2 hitNormal, GameObject source);
}
