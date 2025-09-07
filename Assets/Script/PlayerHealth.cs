using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    public static int maxHealth;
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
    private bool isInvincible = false; 
    private SpriteRenderer playerSprite;
    public bool PanelDeathIsactive;
    public TextMeshProUGUI currentTxt;
    public TextMeshProUGUI maxTxt;
    public bool activeBerserker;
    bool hasTriggerBerserker = false;
    public bool activeInfinity = false;
    private float infinityTimer = 0f;
    private float infinityInterval = 10f; // Toutes les 10 secondes sans être touché
    private bool hasInfinityBonus = false; // Pour suivre si un bonus est actif
    public float damageFois = 1f;
    public Image blackFadeImage;
    public float slowDownDuration = 1.5f;
    public float fadeDuration = 1f;
    public static bool hasBalanceHealth;
    private bool twiceBalance;
    float balanceDamageBonus = 0f;
    float balanceDamageReduction = 0f;
    public XpManagerDeath xpManager;
    public static bool doubleDamage;

    int checkDeath;
    void Start()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        hasBalanceHealth = false;
        doubleDamage = false;
        checkDeath = 0;
        damageFois = 1f;
        activeBerserker = false;
        activeInfinity = false;
        if (blackFadeImage != null)
            blackFadeImage.color = new Color(0f, 0f, 0f, 0f);
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
        float percentageHealth = (float)currentHealth / (float)maxHealth;
        if (percentageHealth <= 0.3f && activeBerserker && !hasTriggerBerserker)
        {
            PlayerStats.additionalDamage += 0.1f;
            hasTriggerBerserker = true;
        }
        else if (percentageHealth > 0.3f && hasTriggerBerserker && activeBerserker)
        {
            PlayerStats.additionalDamage -= 0.1f;
            hasTriggerBerserker = false;
        }
        UpdateBalanceEffect();
        if (healthSlider.maxValue != maxHealth)
            healthSlider.maxValue = maxHealth;
        // Gestion du Power-Up Infinity
        if (activeInfinity)
        {
            infinityTimer += Time.unscaledDeltaTime;
            if (infinityTimer >= infinityInterval)
            {
                PlayerStats.additionalDamage += 0.01f;
                PlayerStats.attackSpeed+= 0.01f;
                infinityTimer = 0f; // Reset timer après ajout
                hasInfinityBonus = true;
            }
        }

        if (isDead >= 1)
        {        
            save.SaveData();
            save.LoadData();
            player.SetActive(false);

            // Vérifie immédiatement la sauvegarde
            PanelDeathIsactive = true;
            Debug.Log("TotalSouls après chargement: " + save.totalSouls);
        }

        DeathControl();
        float targetValue = currentHealth;
        float smoothSpeed = 10f; // Plus la valeur est grande, plus ça va vite
        healthSlider.value = Mathf.Lerp(healthSlider.value, targetValue, Time.deltaTime * smoothSpeed);
        if (healthRegen == 0)
            return;
        healthRegenCooldown -= Time.deltaTime;
        if(healthRegenCooldown <= 0)
        {
            healthRegenCooldown = 1f;
            Heal(healthRegen);
        }

    }
    void GrantXPOnDeath()
    {
        float baseXP = 10 * (staticRef.wavesS + 1) + killAmountStats.totalKill / 3f + (PlayerXPManager.CurrentLevel)*10;

        float xpEarned = baseXP * (save.percentageBonus/100);
        xpManager.AddXP(xpEarned);
        //save.xp += Mathf.FloorToInt(xpEarned);
        Debug.Log($"XP gagné : {xpEarned} | Total XP : {save.xp}");

        save.SaveData();
    }

    void UpdateBalanceEffect()
    {
        if (!hasBalanceHealth) return;

        float coefficientHealth = currentHealth / maxHealth;

        PlayerStats.additionalDamage -= balanceDamageBonus;
        damageFois += balanceDamageReduction;

        balanceDamageBonus = 0f;
        balanceDamageReduction = 0f;

        if (coefficientHealth > 0.55f)
        {
            balanceDamageBonus = 0.075f;
        }
        else if (coefficientHealth < 0.45f)
        {
            balanceDamageReduction = 0.075f;
        }
        else
        {
            balanceDamageReduction = 0.075f;
            balanceDamageBonus = 0.075f;

        }

        PlayerStats.additionalDamage += balanceDamageBonus;
        damageFois -= balanceDamageReduction;
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            damage *= damageFois;
            currentHealth -= (int)damage;
            DeathControl();
            if (currentHealth > 0)
                StartCoroutine(InvincibilityCoroutine());

            // Réinitialiser le bonus Infinity si touché
            if (activeInfinity && hasInfinityBonus)
            {
                ResetInfinityBonus();
            }
        }
    }
    private void ResetInfinityBonus()
    {
        int bonusCount = Mathf.FloorToInt(infinityTimer / infinityInterval); 
        PlayerStats.additionalDamage -= 0.01f * bonusCount;
        PlayerStats.attackSpeed -= 0.01f * bonusCount;
        infinityTimer = 0f;
        hasInfinityBonus = false;
    }
    void DeathControl()
    {
        if (currentHealth <= 0 && !ally2.canRevive && checkDeath == 0)
        {

            StartCoroutine(DeathSequence());
        }
        else if(ally2.canRevive)
        {
            player.SetActive(true);
            deathPanel.SetActive(false);
            FriendHolder.SetActive(true);
        }
    }
    private IEnumerator DeathSequence()
    {
        float elapsed = 0f;
        float initialTimeScale = Time.timeScale;
        while (elapsed < slowDownDuration)
        {
            Time.timeScale = Mathf.Lerp(initialTimeScale, 0.1f, elapsed / slowDownDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 0.1f;

        elapsed = 0f;
        Color fadeColor = blackFadeImage.color;
        while (elapsed < fadeDuration)
        {
            fadeColor.a = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            blackFadeImage.color = fadeColor;
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        isDead++;
        fadeColor.a = 1f;
        blackFadeImage.color = fadeColor;
        Time.timeScale = 0f;
        deathPanel.SetActive(true);
        healthSlider.gameObject.SetActive(false);
        FriendHolder.SetActive(false);
        Cursor.visible = true;
        GrantXPOnDeath();
        checkDeath++;

    }

    public void Heal(int amount)
    {
        print(amount);
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
