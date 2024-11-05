using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public float shieldDuration = 5f; // Durasi perisai aktif
    public GameObject shieldEffect; // Prefab efek visual perisai

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TankShield tankShield = other.GetComponent<TankShield>();
            if (tankShield != null)
            {
                tankShield.ActivateShield(shieldDuration, shieldEffect);
            }

            // Hancurkan power-up setelah diambil
            Destroy(gameObject);
        }
    }
}
