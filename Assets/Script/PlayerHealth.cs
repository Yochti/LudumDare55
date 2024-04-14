using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject deathPanel;
    public GameObject FriendHolder;
    public GameObject player;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            player.SetActive(false);
            deathPanel.SetActive(true);
            FriendHolder.SetActive(false);
        }
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

}
