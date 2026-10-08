using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Vector2 respawnPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            other.transform.position = respawnPosition;

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }
}