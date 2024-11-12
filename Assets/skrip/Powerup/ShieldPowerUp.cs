using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public float shieldDuration = 5f;
    public GameObject shieldEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TankShield tankShield = other.GetComponent<TankShield>();
            if (tankShield != null)
            {
                tankShield.ActivateShield(shieldDuration, shieldEffect);
            }

            Destroy(gameObject);
        }
    }
}
