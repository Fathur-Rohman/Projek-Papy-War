using System.Collections;
using UnityEngine;
using Alteruna;

public class RocketPowerUp : AttributesSync
{
    public GameObject rocketPrefab;       // Prefab for rocket
    public float powerUpDuration = 10f;   // Duration of power-up effect in seconds
    public static Alteruna.Avatar powerUpOwnerAvatar; // Static reference to the avatar of the player who picked up the power-up

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure power-up can only be picked up by players (tanks)
        if (other.CompareTag("Player"))
        {
            Alteruna.Avatar avatar = other.GetComponent<Alteruna.Avatar>();
            if (avatar != null)
            {
                powerUpOwnerAvatar = avatar; // Set the static reference to this avatar
                StartCoroutine(PickUp(other)); // Start power-up effect
            }
        }
    }

    IEnumerator PickUp(Collider2D player)
    {
        // Get TankShooting component from the player
        TankShooting tankShooting = player.GetComponent<TankShooting>();

        if (tankShooting == null)
        {
            Debug.LogWarning("TankShooting component not found on player!");
            yield break;
        }

        // Store the original bullet prefab
        GameObject originalBulletPrefab = tankShooting.bulletPrefab;

        // Replace tank's bullet with rocket
        tankShooting.bulletPrefab = rocketPrefab;

        // Disable power-up display
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // Wait until power-up duration ends
        yield return new WaitForSeconds(powerUpDuration);

        // Restore the original bullet
        tankShooting.bulletPrefab = originalBulletPrefab;

        // Destroy power-up after use
        Destroy(gameObject);
    }
}
