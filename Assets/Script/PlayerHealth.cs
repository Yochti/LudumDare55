using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    [HideInInspector]  public static int maxHealth;
    public int currentHealth;
    public float invicibilityFlashDelay;
    public float invicibilityFlashDelay2 = .05f;
    public GameObject deathPanel;
    public GameObject FriendHolder;
    public GameObject player;
    public AllyHealer ally2;
    public Slider healthSlider;
    public saveSytem save;
    public static int finalSouls;
    public int isDead;
    public int healthRegen;
    public float healthRegenCooldown;
    public ParticleSystem healParticle;
    private bool isInvincible = false; // Pour contrôler l'état d'invincibilité
    private SpriteRenderer playerSprite;
    public bool PanelDeathIsactive;
    public TextMeshProUGUI currentTxt;
    public TextMeshProUGUI maxTxt;
    void Start()
    {

        invicibilityFlashDelay = PlayerStats.invincibilityFrame;
        healthRegen = PlayerStats.healthRegen;
        if (save.HealthUpgrades != 0) maxHealth = PlayerStats.health;
        else maxHealth = 150;

        isDead = 0;
        currentHealth = maxHealth;
        playerSprite = player.GetComponent<SpriteRenderer>();
        print(maxHealth);
    }

    private void Update()
    {
        currentTxt.text = currentHealth.ToString();
        maxTxt.text = maxHealth.ToString();
        if (healthSlider.maxValue != maxHealth)
            healthSlider.maxValue = maxHealth;
        finalSouls = PlayerSoulsCollect.soulValue + 25 * (staticRef.wavesS + 1) + killAmountStats.totalKill/2;

        if (isDead == 1)
        {        
            save.SaveData();
            save.LoadData();
            player.SetActive(false);

            // Vérifie immédiatement la sauvegarde
            PanelDeathIsactive = true;
            Debug.Log("TotalSouls après chargement: " + save.TotalSouls);
        }

        DeathControl();
        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }
        if (healthRegen == 0)
            return;
        healthRegenCooldown -= Time.deltaTime;
        if(healthRegenCooldown <= 0)
        {
            healthRegenCooldown = 1f;
            Heal(healthRegen);
        }
        

        
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible) // Si le joueur n'est pas invincible, il peut prendre des dégâts
        {
            currentHealth -= (int)damage;
            DeathControl();
            if(currentHealth > 0)
                StartCoroutine(InvincibilityCoroutine());

        }
    }

    void DeathControl()
    {
        if (currentHealth <= 0 && !ally2.canRevive)
        {
            isDead++;
            
            healthSlider.value = 0;
            healthSlider.gameObject.SetActive(false);
            deathPanel.SetActive(true);
            FriendHolder.SetActive(false);
            Cursor.visible = true;
        }
        else
        {
            player.SetActive(true);
            deathPanel.SetActive(false);
            FriendHolder.SetActive(true);
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (amount > 10 && healParticle != null)
            healParticle.Play();

    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        float elapsed = 0f;

        while (elapsed < invicibilityFlashDelay)
        {
            
            playerSprite.enabled = !playerSprite.enabled; // Fait clignoter le joueur
            yield return new WaitForSeconds(invicibilityFlashDelay2);
            elapsed += invicibilityFlashDelay2;
        }

        playerSprite.enabled = true; // Assure que le sprite est visible à la fin
        isInvincible = false;
    }

}
