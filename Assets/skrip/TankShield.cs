using UnityEngine;

public class TankShield : MonoBehaviour
{
    private bool shieldActive = false;
    private GameObject currentShieldEffect;

    public void ActivateShield(float duration, GameObject shieldEffectPrefab)
    {
        if (!shieldActive)
        {
            shieldActive = true;
            currentShieldEffect = Instantiate(shieldEffectPrefab, transform.position, Quaternion.identity);
            currentShieldEffect.transform.parent = transform;
            Invoke("DeactivateShield", duration);
        }
    }

    private void DeactivateShield()
    {
        shieldActive = false;
        if (currentShieldEffect != null)
        {
            Destroy(currentShieldEffect);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (shieldActive && collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
