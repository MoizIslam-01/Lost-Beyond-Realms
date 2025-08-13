using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Combat")]
    public float Health = 10f;
    public float energy = 0f;

    [Header("Movement")]
    public float moveSpeed = 4f;
    public float jumpForce = 7f;  // force for jumping
    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector2 lastDirection = Vector2.right; // default to right

    [Header("Shooting")]
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float firePointDistance = 0.25f;

    [Header("Deflecting")]
    public GameObject deflectPerfab;
    public Transform deflectpoint;

    [Header("Final Attack")]
    public GameObject finalprefab1;
    public GameObject finalprefab2;
    public GameObject finalprefab3;
    public GameObject finalprefab4;
    public Transform finalpoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // --- LEFT & RIGHT MOVEMENT ---
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);

        // --- SET LAST DIRECTION FOR SHOOTING ---
        if (horizontal > 0)
            lastDirection = Vector2.right;
        else if (horizontal < 0)
            lastDirection = Vector2.left;

        // --- UPDATE FIREPOINT POSITION ---
        if (firePoint != null)
            firePoint.localPosition = lastDirection * firePointDistance;

        // --- JUMP ---
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)

        {
            rb.gravityScale = 0f;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
            isGrounded = false;
        }
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = 3f; // stronger gravity going down
        }
        else
        {
            rb.gravityScale = 1f; // normal gravity going up
        }

        // --- SHOOT ---
        if (Input.GetKeyDown(KeyCode.E))
            Shoot();

        if (Input.GetKeyDown(KeyCode.Q))
            Deflect();
            FinalAtk();

        if (Input.GetKeyDown(KeyCode.T))
            GetEnergy();
              
    }
    void TakeDmg(){
        Health -=1;
    }

    void GetEnergy()
    {
        energy *=1;
    }

    void FinalAtk(){
        if (energy == 2)
        {
            if (finalprefab1 == null) return;
            GameObject final = Instantiate(finalprefab1, finalpoint.position, Quaternion.identity);
        }
        if (energy == 4)
        {
            if (finalprefab2 == null) return;
            GameObject final = Instantiate(finalprefab2, finalpoint.position, Quaternion.identity);
        }
        if (energy == 6)
        {
            if (finalprefab3 == null) return;
            GameObject final = Instantiate(finalprefab3, finalpoint.position, Quaternion.identity);
        }
        if (energy == 8)
        {
            if (finalprefab4 == null) return;
            GameObject final = Instantiate(finalprefab4, finalpoint.position, Quaternion.identity);
        }
    }

    void Deflect()
    {
        if (deflectPerfab == null) return;

        GameObject shield = Instantiate(deflectPerfab, deflectpoint.position, Quaternion.identity);
        Destroy(shield,0.1f);
    }

    void Shoot()
    {
        if (fireballPrefab == null || firePoint == null) return;

        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        fireball.GetComponent<Fireball>().SetDirection(lastDirection);
    }

    // --- CHECK IF PLAYER IS GROUNDED ---
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Shoot();
        }
    }

    public Vector2 GetLastDirection()
    {
        return lastDirection;
    }
}
