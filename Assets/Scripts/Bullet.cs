using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 20f;
    public float lifetime = 5f;
    public int damage = 1;
    public bool isPlayerBullet = true;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;
        rb.linearVelocity = Vector3.forward * speed;

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isPlayerBullet)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Obstacle"))
            {
                Destructible dest = other.GetComponent<Destructible>();
                if (dest != null)
                {
                    dest.TakeDamage(damage);
                }

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.AddScore(10);
                }

                Destroy(gameObject);
            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                PlayerController player = other.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                }
                Destroy(gameObject);
            }
        }

        if (other.CompareTag("Obstacle") || other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}
