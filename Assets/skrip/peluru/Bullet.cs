using UnityEngine;
using Alteruna;

public class Bullet : AttributesSync
{
    public float speed = 20f;
    public int damage = 1;
    private Vector3 velocity;

    public void Initialize(Vector3 initialVelocity)
    {
        velocity = initialVelocity;
        SyncDirection(velocity);  // Sync the bullet's velocity across clients
    }

    [SynchronizableMethod]
    public void SyncDirection(Vector3 syncVelocity)
    {
        velocity = syncVelocity;
    }

    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.collider.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            Destroy(gameObject);  // Destroy bullet after hit
        }
    }
}
