using UnityEngine;

public class HealBullet : MonoBehaviour
{
    public int healAmount = 1; // Quantité de soin à appliquer au joueur
    private playerBulletDamage hInt;

    private void Start()
    {
        hInt = FindObjectOfType<playerBulletDamage>(); 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealPlayer(other.gameObject);
            Destroy(gameObject);
            hInt.onHitHeal(healAmount);
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
