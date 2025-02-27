using UnityEngine;
using UnityEngine.UI;

public class Boss1Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public AudioSource deathSFX;
    public ParticleSystem explosionVFXPrefab; // Prefab de l'effet d'explosion
    public float explosionDelay = 0.1f; // Délai avant la destruction de l'objet après l'explosion
    public int scorePoint;
    private Slider healthSlider;
    private Slider easeSlider;
    private float lerpSpeed = 0.01f;
    public bool hasBeenHit;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        easeSlider = GameObject.Find("EaseSlider").GetComponent<Slider>();
        healthSlider.maxValue = maxHealth;
        easeSlider.maxValue = maxHealth;
    }
    private void Update()
    {
        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;

        }
        if (healthSlider.value != easeSlider.value)
        {
            easeSlider.value = Mathf.Lerp(easeSlider.value, currentHealth, lerpSpeed);
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {

            Die();
        }
    }
    public void Heal(int heal)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += heal;
        }
        else
            currentHealth = maxHealth;
    }

    void Die()
    {
        if (explosionVFXPrefab != null)
        {
            Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
        }

        deathSFX.Play();
        CameraShake.Instance.ShakeCamera(3f, 0.1f);
        Score.ScoreCount += scorePoint;

        Destroy(gameObject, explosionDelay);

    }
}
