using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageDealer2D : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask targetLayers;   // e.g. Player/Hurtbox layer(s)
    [SerializeField] private float hitCooldown = 0.15f; // prevent multi-hits in same frame

    private readonly Dictionary<IDamageable, float> _lastHit = new();

    void Reset()
    {
        // Ensure trigger
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        TryDeal(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // Optional: allow ticks when staying inside
        TryDeal(other);
    }

    private void TryDeal(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & targetLayers.value) == 0)
            return;

        // Find damageable component up the hierarchy (player might be child object)
        var dmg = other.GetComponentInParent<IDamageable>();
        if (dmg == null || !dmg.IsAlive) return;

        // Simple per-target cooldown
        float now = Time.time;
        if (_lastHit.TryGetValue(dmg, out float last) && now - last < hitCooldown)
            return;

        var hitPoint = other.ClosestPoint(transform.position);
        var hitNormal = (Vector2)(other.transform.position - transform.position).normalized;

        dmg.TakeDamage(damage, hitPoint, hitNormal, gameObject);
        _lastHit[dmg] = now;
    }
}
