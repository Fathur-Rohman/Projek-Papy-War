using System.Collections;
using UnityEngine;

public class RocketPowerUp : MonoBehaviour
{
    public GameObject rocketPrefab;       // Prefab untuk roket
    public float powerUpDuration = 10f;   // Durasi efek power-up dalam detik

    void OnTriggerEnter2D(Collider2D other)
    {
        // Memastikan power-up hanya dapat diambil oleh player (tank)
        if (other.CompareTag("Player"))
        {
            // Mulai efek power-up
            StartCoroutine(PickUp(other));
        }
    }

    IEnumerator PickUp(Collider2D player)
    {
        // Ambil komponen TankShooting pada player
        TankShooting tankShooting = player.GetComponent<TankShooting>();

        if (tankShooting == null)
        {
            Debug.LogWarning("TankShooting component not found on player!");
            yield break;
        }

        // Simpan peluru asli tank sebelum diganti
        GameObject originalBulletPrefab = tankShooting.bulletPrefab;

        // Ganti peluru tank dengan roket
        tankShooting.bulletPrefab = rocketPrefab;

        // Nonaktifkan tampilan power-up
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // Tunggu hingga durasi power-up berakhir
        yield return new WaitForSeconds(powerUpDuration);

        // Kembalikan peluru asli
        tankShooting.bulletPrefab = originalBulletPrefab;

        // Hancurkan power-up setelah selesai
        Destroy(gameObject);
    }
}
