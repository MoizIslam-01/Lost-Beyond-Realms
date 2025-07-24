using UnityEngine;

public class PartnerFollow : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 3f;
    public float followDistance = 1.5f;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > followDistance)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                followSpeed * Time.deltaTime
            );
        }
    }
}
