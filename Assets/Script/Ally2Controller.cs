using UnityEngine;

public class AllyHealer : MonoBehaviour
{
    public Transform player;
    public AlliesSummon ally;
    public PlayerHealth PlayerH;
    public float moveSpeed = 5f; 
    public float maxDistanceFromPlayer = 5f; 
    public float healingInterval = 30f;
    public AudioSource healSFX;
    public AudioSource reviveSFX;
    private float lastHealTime;
    public bool canRevive;
    public float countdown;
    private playerBulletDamage hInt;
    void Start()
    {
        hInt = FindObjectOfType<playerBulletDamage>();
        lastHealTime = Time.time;
        countdown = 600f;
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
        revivePlayer();
    }
    void revivePlayer()
    {
        if (ally.lvlToRevive)
        {
            countdown -= Time.deltaTime;

            if (countdown <= 0)
            {
                canRevive = true;
            }
        }
        if (PlayerH.currentHealth <= 0 && canRevive)
        {
                    PlayerH.currentHealth = PlayerHealth.maxHealth/3;
                    countdown = 600f;
                    canRevive = false;
                    reviveSFX.Play();
        }
            
            
        
            
    }
    void HealPlayer()
    {
        
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            int missingHealth = PlayerHealth.maxHealth - playerHealth.currentHealth;
            int healingAmount = missingHealth / 4 + missingHealth / 6;
            playerHealth.Heal(healingAmount);
            hInt.hName(healingAmount);
        }
    }
}
