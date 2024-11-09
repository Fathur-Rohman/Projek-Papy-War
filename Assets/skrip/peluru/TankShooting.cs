using UnityEngine;
using Alteruna;

public class TankShooting : AttributesSync
{
    public GameObject bulletPrefab;   // Reference to bullet prefab
    public Transform shootPoint;      // Spawn point for the bullet
    public float bulletSpeed = 20f;
    public float shootCooldown = 0.5f;
    private float lastShootTime;

    private Alteruna.Avatar avatar;

    void Start()
    {
        avatar = GetComponent<Alteruna.Avatar>();
    }

    void Update()
    {
        if (avatar.IsMe && Input.GetKeyDown(KeyCode.Space) && Time.time > lastShootTime + shootCooldown)
        {
            lastShootTime = Time.time;
            Shoot();
        }
    }

    void Shoot()
    {
        SyncShoot(shootPoint.position, shootPoint.up * bulletSpeed);
    }

    [SynchronizableMethod]
    void SyncShoot(Vector3 position, Vector3 velocity)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Initialize(velocity);  // Set synchronized direction
        Destroy(bullet, 3f);
    }
}
