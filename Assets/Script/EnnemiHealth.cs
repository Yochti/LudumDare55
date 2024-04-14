using UnityEngine;

public class EnemmiHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public AudioSource deathSFX;
    public ParticleSystem explosionVFXPrefab; // Prefab de l'effet d'explosion
    public float explosionDelay = 0.1f; // Délai avant la destruction de l'objet après l'explosion

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (explosionVFXPrefab != null)
        {
            Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
        }
        deathSFX.Play();
        CameraShake.Instance.ShakeCamera(3f, 0.1f);
        Score.ScoreCount += 10;
        Destroy(gameObject, explosionDelay);
    }
}
