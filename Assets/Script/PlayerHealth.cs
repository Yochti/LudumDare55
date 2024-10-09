using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector]  public int maxHealth;
    public int currentHealth;
    public float invicibilityFlashDelay;
    public float invicibilityFlashDelay2 = .05f;
    public GameObject deathPanel;
    public GameObject FriendHolder;
    public GameObject player;
    public AllyHealer ally2;
    public Slider healthSlider;
    public saveSytem save;
    public int finalSouls;
    public int isDead;
    public int healthRegen;
    public float healthRegenCooldown;

    private bool isInvincible = false; // Pour contrôler l'état d'invincibilité
    private SpriteRenderer playerSprite;

    void Start()
    {
        if (save.HealthRegen != 0)
            healthRegen = save.PlayerRegen;
        else
            healthRegen = 0;
        if (save.PlayerFrame != 0)
            invicibilityFlashDelay = save.PlayerFrame;
        else
            invicibilityFlashDelay = .4f;

        if (save.PlayerHealth != 0)
            maxHealth = save.PlayerHealth;
        else
            maxHealth = 200;

        isDead = 0;
        currentHealth = maxHealth;
        playerSprite = player.GetComponent<SpriteRenderer>();
        print(maxHealth);
    }

    private void Update()
    {
        healthRegenCooldown -= Time.deltaTime;
        if(healthRegenCooldown <= 0)
        {
            healthRegenCooldown = 1f;
            Heal(healthRegen);
        }
        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }

        if (healthSlider.maxValue != maxHealth)
            healthSlider.maxValue = maxHealth;

        if (isDead == 1)
        {
            finalSouls = PlayerSoulsCollect.soulValue;
            save.TotalSouls += PlayerSoulsCollect.soulValue;
            save.SaveData();
            save.LoadData(); // Vérifie immédiatement la sauvegarde
            Debug.Log("TotalSouls après chargement: " + save.TotalSouls);
        }

        DeathControl();
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible) // Si le joueur n'est pas invincible, il peut prendre des dégâts
        {
            currentHealth -= (int)damage;
            DeathControl();
            StartCoroutine(InvincibilityCoroutine()); // Lance la coroutine d'invincibilité
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
            //player.SetActive(false);
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
