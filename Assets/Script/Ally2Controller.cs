using UnityEngine;

public class AllyHealer : MonoBehaviour
{
    public Transform player; 
    public float moveSpeed = 5f; 
    public float maxDistanceFromPlayer = 5f; 
    public float healingInterval = 30f;
    public AudioSource healSFX;
    private float lastHealTime; 

    void Start()
    {
        lastHealTime = Time.time;
    }

    void Update()
    {
        
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) > maxDistanceFromPlayer)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }

        if (Time.time - lastHealTime >= healingInterval && player != null)
        {
            HealPlayer();
            healSFX.Play();
            lastHealTime = Time.time;
        }
    }

    void HealPlayer()
    {
        
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            int missingHealth = playerHealth.maxHealth - playerHealth.currentHealth;
            int healingAmount = missingHealth / 2;

            
            playerHealth.Heal(healingAmount);
        }
    }
}
