using UnityEngine;
using Alteruna;

public class EnemyBehavior : AttributesSync
{
    [SynchronizableField] public int health = 5;
    private Alteruna.Avatar _avatar;
    public ParticleSystem particleSystemPrefab;

    void Start()
    {
        _avatar = GetComponent<Alteruna.Avatar>();

        if (!_avatar.IsMe)
            return;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_avatar.IsMe)
            return;
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;

            Destroy(collision.gameObject);

            if (health <= 0)
            {
                BroadcastRemoteMethod("Kill");
            }
        }
    }

    [SynchronizableMethod]
    public void Kill()
    {
        Destroy(gameObject);
        Instantiate(particleSystemPrefab, transform.position, Quaternion.identity).Play();

    }

    public void SyncKill()
    {
        BroadcastRemoteMethod("Kill");
    }
}