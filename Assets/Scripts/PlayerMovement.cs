using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastDirection = Vector2.right; // default to right

    [Header("Shooting")]
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float firePointDistance = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get raw movement input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movement = new Vector2(horizontal, vertical);

        // Update lastDirection based on any horizontal input (even when moving diagonally)
        if (horizontal > 0)
            lastDirection = Vector2.right;
        else if (horizontal < 0)
            lastDirection = Vector2.left;

        // Position FirePoint based on lastDirection
        if (firePoint != null)
            firePoint.localPosition = lastDirection * firePointDistance;

        // Shooting on Space key press
        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();
    }

    

    void Shoot()
    {
        if (fireballPrefab == null || firePoint == null) return;

        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        fireball.GetComponent<Fireball>().SetDirection(lastDirection);
    }

    public Vector2 GetLastDirection()
    {
        return lastDirection;
    }
}
