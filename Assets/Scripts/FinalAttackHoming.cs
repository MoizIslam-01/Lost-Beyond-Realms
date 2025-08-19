using UnityEngine;

public class FinalAttackHoming : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    void Update()
    {
        if (target == null) return;

        // Move towards enemy
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Optional: destroy when close enough
        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            //Debug.Log("Final attack hit enemy!");
            Destroy(gameObject); // or trigger enemy damage here
        }
    }
}
