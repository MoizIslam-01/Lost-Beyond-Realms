using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase2D : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private float stopDistance = 0.15f;

    private Rigidbody2D _rb;
    private Transform _target;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f; // top-down
        _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void Start()
    {
        var t = GameObject.FindGameObjectWithTag(targetTag);
        if (t != null) _target = t.transform;
    }

    void FixedUpdate()
    {
        if (_target == null) return;

        Vector2 to = _target.position - transform.position;
        float dist = to.magnitude;
        if (dist <= stopDistance)
        {
            _rb.velocity = Vector2.zero;
            return;
        }

        Vector2 dir = to / Mathf.Max(0.0001f, dist);
        _rb.velocity = dir * moveSpeed;
    }
}
