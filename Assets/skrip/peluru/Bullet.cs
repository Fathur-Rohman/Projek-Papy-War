using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        // Hancurkan peluru setelah 5 detik
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Jika Anda ingin peluru tetap hancur saat bertabrakan, tambahkan ini
        // Destroy(gameObject);
    }
}
