using UnityEngine;
using Alteruna;

public class HomingMissile : AttributesSync
{
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public GameObject explosionPrefab;
    [SerializeField] private Transform target;
    public LayerMask playerLayer;

    void Start()
    {
        BroadcastRemoteMethod("FindTarget");
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            BroadcastRemoteMethod("FindTarget");
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
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    [SynchronizableMethod]
    void LaunchRocket()
    {
        if (target == null) return;
        Debug.Log(target.name);

        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        GetComponent<Rigidbody2D>().angularVelocity = -rotateAmount * rotateSpeed;

        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }
}
