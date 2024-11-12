using System.Collections;
using UnityEngine;
using Alteruna;

public class RocketPowerUp : AttributesSync
{
    public GameObject rocketPrefab;
    public float powerUpDuration = 10f;
    public static Alteruna.Avatar powerUpOwnerAvatar;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Alteruna.Avatar avatar = other.GetComponent<Alteruna.Avatar>();
            if (avatar != null)
            {
                powerUpOwnerAvatar = avatar;
                StartCoroutine(PickUp(other));
            }
        }
    }

    IEnumerator PickUp(Collider2D player)
    {
        TankShooting tankShooting = player.GetComponent<TankShooting>();

        if (tankShooting == null)
        {
            Debug.LogWarning("TankShooting component not found on player!");
            yield break;
        }

        GameObject originalBulletPrefab = tankShooting.bulletPrefab;

        tankShooting.bulletPrefab = rocketPrefab;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(powerUpDuration);

        tankShooting.bulletPrefab = originalBulletPrefab;

        Destroy(gameObject);
    }
}
