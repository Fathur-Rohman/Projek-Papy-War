using UnityEngine;
using Alteruna;

public class Bullet : AttributesSync
{
    [SerializeField] private Alteruna.Avatar _avatar;

    void Start()
    {
        if (_avatar != null && !_avatar.IsMe)
            return;

        Destroy(gameObject, 5f);
    }

    [SynchronizableMethod]
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_avatar.IsMe)
            return;
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
