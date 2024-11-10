using UnityEngine;
using Alteruna;

public class HomingMissile : AttributesSync
{
    public float speed = 5f; // Rocket speed
    public float rotateSpeed = 200f; // Rocket rotation speed
    public GameObject explosionPrefab; // Explosion effect prefab
    [SerializeField] private Transform target;
    public LayerMask playerLayer; // Layer for target objects

    void Start()
    {
        // Find a target with the tag "Player" that is not the power-up owner
        BroadcastRemoteMethod("FindTarget");
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            BroadcastRemoteMethod("FindTarget"); // Search again if the target is lost
            return;
        }
        BroadcastRemoteMethod("LaunchRocket");
    }

    [SynchronizableMethod]
    private void FindTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Alteruna.Avatar avatar = player.GetComponent<Alteruna.Avatar>();
            // Ensure the target is not the player who picked up the power-up
            if (avatar != null && avatar != RocketPowerUp.powerUpOwnerAvatar)
            {
                target = player.transform;
                break;
            }
        }
    }

    [SynchronizableMethod]
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            EnemyBehavior enemyBehavior = collision.gameObject.GetComponent<EnemyBehavior>();
            BroadcastRemoteMethod("Kill");
            enemyBehavior.SyncKill();
        }
    }

    [SynchronizableMethod]
    void Kill()
    {
        // Display explosion effect
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject); // Destroy rocket
    }

    [SynchronizableMethod]
    void LaunchRocket()
    {
        if (target == null) return; // Ensure there is a target
        Debug.Log(target.name);

        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        // Rotate rocket
        GetComponent<Rigidbody2D>().angularVelocity = -rotateAmount * rotateSpeed;

        // Move rocket towards the target
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }
}
