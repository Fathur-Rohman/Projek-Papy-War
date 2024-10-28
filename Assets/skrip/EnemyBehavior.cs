using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int health = 1; // Kesehatan enemy, bisa diatur sesuai keinginan

    void OnCollisionEnter2D(Collision2D collision)
    {
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
                Destroy(gameObject);
            }
        }
    }
}
