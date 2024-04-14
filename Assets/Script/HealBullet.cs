using UnityEngine;

public class HealBullet : MonoBehaviour
{
    public int healAmount = 1; // Quantit� de soin � appliquer au joueur

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealPlayer(other.gameObject);
            Destroy(gameObject); // D�truit la balle de soin apr�s avoir touch� le joueur
        }
    }

    void HealPlayer(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
        }
    }
}
