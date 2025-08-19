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

    [Header("Falling Atack")]
    public Transform Fallingpoint;
    public GameObject Fallingprefab;

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
        if (rb.linearVelocity.y != 0 && Input.GetKeyDown(KeyCode.T))
        {
            rb.gravityScale = 0f;
            rb.AddForce(Vector2.down * 20, ForceMode2D.Impulse);
            Falling();
        }
        else if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = 4f; // stronger gravity going down
        }
        else
        {
            rb.gravityScale = 1f; // normal gravity going up
        }

        // --- SHOOT ---
        if (Input.GetKeyDown(KeyCode.E))
            Shoot();

        if (Input.GetKeyDown(KeyCode.Q))
        {

            Deflect();

        }
        
                          
    }
    public void TakeDmg()
    {
        Health -= 1;
        Debug.Log("Player Health: " + Health);
        if (Health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        // Tell GameManager that player has died
        GameManager.Instance.GameOver();
    }

    public void GetEnergy()
    {
        energy += 1;
        if (energy > 8) energy = 0; // cap energy at 8
        Debug.Log("Player Energy: " + energy);
    }

    private GameObject currentFinalAttack; // store the active attack

    public void FinalAtk()
{
    GameObject prefabToSpawn = null;

    if (energy == 2)
        prefabToSpawn = finalprefab1;
    else if (energy == 4)
        prefabToSpawn = finalprefab2;
    else if (energy == 6)
        prefabToSpawn = finalprefab3;
    else if (energy == 8)
        prefabToSpawn = finalprefab4;

    if (prefabToSpawn != null)
    {
        if (currentFinalAttack != null)
            Destroy(currentFinalAttack);

        currentFinalAttack = Instantiate(prefabToSpawn, finalpoint.position, Quaternion.identity);

        // ✅ If it's the final stage (energy == 8), make it chase the enemy
        if (energy == 8)
        {
            FinalAttackHoming homing = currentFinalAttack.AddComponent<FinalAttackHoming>();
            homing.speed = 5f; // adjust as needed
            homing.target = GameObject.FindGameObjectWithTag("Enemy")?.transform;
        }
        else
        {
            // Otherwise, keep it above player head
            currentFinalAttack.transform.SetParent(transform);
            currentFinalAttack.transform.localPosition = new Vector3(0, 1.5f, 0);
        }
    }
}


    void Falling()
    {
        if (Fallingprefab == null) return;
        GameObject falling = Instantiate(Fallingprefab, Fallingpoint.position, Quaternion.identity);
        falling.transform.SetParent(transform);
        Destroy(falling, 0.2f);
    }
    void Deflect()
    {
        if (deflectPerfab == null) return;

        GameObject shield = Instantiate(deflectPerfab, deflectpoint.position, Quaternion.identity);
        shield.transform.SetParent(deflectpoint);
        Destroy(shield, 0.1f);
        
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

    

    public Vector2 GetLastDirection()
    {
        return lastDirection;
    }
}