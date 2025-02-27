using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemmiHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public AudioSource deathSFX;
    public ParticleSystem explosionVFXPrefab; // Prefab de l'effet d'explosion
    public float explosionDelay = 0.1f; // D�lai avant la destruction de l'objet apr�s l'explosion
    public int scorePoint;
    private float lerpSpeed = 0.008f;
    public SpriteRenderer sprite;
    private Color originalColor;
    private int verificationScore;
    public bool hasBeenHit;
    void Start()
    {
        verificationScore = 0;
        currentHealth = maxHealth;
        originalColor = sprite.color; // Sauvegarde de la couleur originale du sprite
    }

    public void TakeDamage(int damage)
    {
        sprite.color = Color.white; // Change la couleur pour indiquer des d�g�ts
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(waitToColor()); // Appel de la coroutine pour restaurer la couleur d'origine
        }
    }

    public void Heal(int heal)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += heal;
        }
        else
        {
            currentHealth = maxHealth;
        }
    }

    void Die()
    {
        if (explosionVFXPrefab != null)
        {
            Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
        }
        if(verificationScore < 1)
        {
            Score.ScoreCount += scorePoint;
            verificationScore++;
        }
        playerPassif.passifVampire(1);
        deathSFX.Play();
        CameraShake.Instance.ShakeCamera(3f, 0.1f);

        Destroy(gameObject, explosionDelay);
    }

    IEnumerator waitToColor()
    {
        yield return new WaitForSeconds(0.1f); // Attend un court d�lai

        sprite.color = originalColor;
    }
}
