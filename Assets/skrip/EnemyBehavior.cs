using UnityEngine;
using Alteruna;

public class EnemyBehavior : AttributesSync
{
    [SynchronizableField] public int health = 1; // Kesehatan enemy, bisa diatur sesuai keinginan
    private Alteruna.Avatar _avatar;

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
        // Jika enemy terkena peluru
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Kurangi kesehatan
            health--;

            // Hancurkan peluru setelah bertabrakan
            Destroy(collision.gameObject);

            // Jika kesehatan mencapai 0, hancurkan enemy
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
    }

    public void SyncKill()
    {
        BroadcastRemoteMethod("Kill");
    }
}