using UnityEngine;
using Alteruna;

public class PlayerHealth : AttributesSync
{
    public int maxHealth = 3;
    private int currentHealth;
    private Alteruna.Avatar _avatar;

    void Start()
    {
        _avatar = GetComponent<Alteruna.Avatar>();

        if  (!_avatar.IsMe)
            return;
        currentHealth = maxHealth;  // Initialize health at max
    }

    // Method to apply damage, which will be synchronized
    [SynchronizableMethod]
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("health : " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            SyncHealth(currentHealth);  // Sync current health across clients
        }
    }

    // Synchronize health changes across players
    [SynchronizableMethod]
    void SyncHealth(int syncedHealth)
    {
        currentHealth = syncedHealth;
    }

    // Handle player death
    void Die()
    {
        if  (!_avatar.IsMe)
            return;
        // Here you could add respawn logic or disable the player object
        Debug.Log("Player has died.");
        
        // Ensure RoomMenu component is available and call the leave function
        RoomMenu roomMenu = FindObjectOfType<RoomMenu>();
        if (roomMenu != null)
        {
            roomMenu.Multiplayer.CurrentRoom?.Leave();
        }
    }

    // Optional: Method to heal the player, useful if there are power-ups
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        SyncHealth(currentHealth);  // Sync healing as well
    }
}
