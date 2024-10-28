using System.Collections;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    public GameObject bulletPrefab;   // Prefab untuk peluru
    public Transform firePoint;       // Titik tembak peluru
    public float bulletSpeed = 20f;   // Kecepatan peluru
    public float shootCooldown = 2f;  // Waktu cooldown antara set peluru
    public int shotsPerCooldown = 2;  // Jumlah peluru per setiap cooldown

    private int shotsRemaining;       // Sisa peluru yang bisa ditembakkan dalam cooldown
    private bool canShoot = true;     // Menentukan apakah tank bisa menembak atau tidak

    void Start()
    {
        shotsRemaining = shotsPerCooldown; // Set jumlah peluru awal
    }

    void Update()
    {
        // Deteksi input spasi untuk menembak
        if (Input.GetKeyDown(KeyCode.Space) && canShoot && shotsRemaining > 0)
        {
            Shoot();
            shotsRemaining--;  // Kurangi jumlah peluru yang tersisa
            if (shotsRemaining == 0)
            {
                StartCoroutine(ResetShootCooldown()); // Mulai cooldown setelah batas peluru habis
            }
        }
    }

    void Shoot()
    {
        // Buat peluru dan beri kecepatan
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;

        // Hancurkan peluru setelah 3 detik
        Destroy(bullet, 3f);
    }

    IEnumerator ResetShootCooldown()
    {
        canShoot = false;           // Nonaktifkan kemampuan menembak
        yield return new WaitForSeconds(shootCooldown); // Tunggu waktu cooldown
        shotsRemaining = shotsPerCooldown; // Reset jumlah peluru per cooldown
        canShoot = true;            // Aktifkan kembali kemampuan menembak
    }
}
