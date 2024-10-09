using UnityEngine;

public class Boss5Behaviour: MonoBehaviour
{
    private Transform player; // R�f�rence au joueur
    public GameObject smallEnemyPrefab; // Pr�fabriqu� pour les petits ennemis
    public float moveSpeed = 5f; // Vitesse de d�placement du boss
    public float spawnInterval = 10f; // Intervalle entre chaque spawn de petits ennemis
    public int numberOfSmallEnemies = 25; // Nombre de petits ennemis � instancier � chaque spawn
    private Boss1Health bossHp;
    private EnemyWaveManager waveManager;
    private float nextSpawnTime; // Temps du prochain spawn de petits ennemis
    private Rigidbody2D rb;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        bossHp = GetComponent<Boss1Health>();
        waveManager = FindAnyObjectByType<EnemyWaveManager>();
        AdjustBossHealth(); 
    }
    void Update()
    {
        MoveTowardsPlayer();

        nextSpawnTime -= Time.deltaTime;
        if (nextSpawnTime <= 0)
        {
            SpawnSmallEnemies(); // Instancier les petits ennemis
            nextSpawnTime = 10f;
        }
        
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            rb.velocity = direction * moveSpeed;

        }
    }

    void SpawnSmallEnemies()
    {
        for (int i = 0; i < numberOfSmallEnemies; i++)
        {
            // G�n�rer une position al�atoire devant le boss
            Vector2 randomOffset = Random.insideUnitCircle * 5f; // Position al�atoire dans un rayon de 5 unit�s
            Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

            // Instancier un petit ennemi � cette position
            Instantiate(smallEnemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
    void AdjustBossHealth()
    {
        int currentWave = waveManager.currentWave;
        int baseHealth = bossHp.maxHealth;

        int modifiedHealth = baseHealth + (currentWave * 300);
        bossHp.maxHealth = modifiedHealth;
        bossHp.currentHealth = modifiedHealth;
    }
}
