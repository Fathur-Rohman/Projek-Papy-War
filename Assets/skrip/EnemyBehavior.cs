using UnityEngine;
using Alteruna;

public class EnemyBehavior : AttributesSync
{
    public Transform target;
    public float moveSpeed = 5f;
    public int damage = 1;

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            SyncEnemyMovement(direction);
        }
    }

    [SynchronizableMethod]
    void SyncEnemyMovement(Vector3 direction)
    {
        // Synchronize the enemy movement across all players
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
