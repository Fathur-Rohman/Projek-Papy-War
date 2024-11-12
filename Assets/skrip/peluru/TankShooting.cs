using System.Collections;
using UnityEngine;
using Alteruna;

public class TankShooting : AttributesSync
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float shootCooldown = 2f;
    public int shotsPerCooldown = 2;

    private int shotsRemaining;
    private bool canShoot = true;
    private Alteruna.Avatar _avatar;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int playerSelfLayer;

    void Start()
    {
        _avatar = GetComponent<Alteruna.Avatar>();
        if (_avatar.IsMe)
            _avatar.gameObject.layer = playerSelfLayer;
        shotsRemaining = shotsPerCooldown;
    }

    void Update()
    {
        if (!_avatar.IsMe)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && canShoot && shotsRemaining > 0)
        {
            BroadcastRemoteMethod("SynchronizeShoot");
            shotsRemaining--;
            if (shotsRemaining == 0)
            {
                StartCoroutine(ResetShootCooldown());
            }
        }
    }

    [SynchronizableMethod]
    void SynchronizeShoot()
    {
        Debug.Log("SynchronizeShoot");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;

        Destroy(bullet, 3f);
    }

    IEnumerator ResetShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        shotsRemaining = shotsPerCooldown;
        canShoot = true;
    }
}
