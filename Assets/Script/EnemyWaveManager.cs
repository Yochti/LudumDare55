using UnityEngine;
using System.Collections;
using TMPro;

public class EnemyWaveManager : MonoBehaviour
{
    public GameObject[] enemyPrefab; // Tableau des préfabriqués d'ennemis
    public GameObject[] bossPrefabs5; // Tableau des préfabriqués de boss pour la vague 5
    public GameObject[] bossPrefabs10; // Tableau des préfabriqués de boss pour la vague 10
    public float spawnInterval = 0.1f; // Intervalle entre chaque génération d'ennemi
    public float enemiesPerWave = 5f;
    public float multiplicator = 3.5f;
    public int wavesCount = 50; // Nombre de vagues total
    public float maxSpawnRange = 150f; // Portée maximale de génération
    public TextMeshProUGUI waveText;
    public GameObject player;
    // Script enemy
    public EnemyAI e1;
    public EnemmiHealth eHP;
    public EnemmiHealth eHP2;
    public EnemmiHealth eHP3;
    public EnemmiHealth eHP4;
    public EnemyController IEnemy;
    public BulletLifEnemy eBullet;
    public EnemyAI3 e3;
    public EnemmiHealth eHP5;    public EnemmiHealth eHP6;    public EnemmiHealth eHP6_2;
    public Enemy9AI eDamage;
    public MirageEnemy eDamage2;
    public int currentWave = 0;
    private bool isWaveActive = false;

    void Start()
    {
        StartCoroutine(InitialDelay());
    }

    IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(5f);
        StartNextWave();
    }

    void StartNextWave()
    {
        StartCoroutine(SpawnWave());
        if ((currentWave + 1) == 10 || (currentWave > 0 && (currentWave + 1) % 10 == 0))
        {
            SpawnBoss10();
            IncreaseEnemyStats();

        }
        else if ((currentWave + 1) == 5 || (currentWave > 0 && (currentWave + 1) % 5 == 0))
        {
            SpawnBoss5();
            IncreaseEnemyStats();
        }
    }

    IEnumerator SpawnWave()
    {
        isWaveActive = true;

        SpawnEnemy();
        yield return new WaitForSeconds(spawnInterval);

        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Boss").Length == 0);

        currentWave++;
        isWaveActive = false;

        if (currentWave < wavesCount)
        {
            waveText.gameObject.SetActive(true);
            waveText.text = "Wave " + (currentWave + 1);

            yield return new WaitForSeconds(1.5f);
            waveText.gameObject.SetActive(false);

            yield return new WaitForSeconds(3f);
            StartNextWave();
        }
    }

    void IncreaseEnemyStats()
    {
        e1.damage += 5f;
        e3.explosionDamage += 2.5f;
        eHP.maxHealth += 5;
        eHP2.maxHealth += 3;
        eHP3.maxHealth += 2;
        eHP4.maxHealth += 5;
        eHP5.maxHealth += 5;
        eHP6.maxHealth += 3;
        eHP6_2.maxHealth += 8;
        eBullet.bulletDamage += 4f;
        eDamage.attackDamage += 4;
        eDamage2.attackDamage += 3;eDamage2.illusionDamage += 2;
        IEnemy.invisibilityDuration += 0.5f;
        IEnemy.moveSpeed += 0.25f;
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < enemiesPerWave + (currentWave * multiplicator); i++)
        {
            int randomIndex = Random.Range(0, enemyPrefab.Length);
            Vector2 randomOffset = Random.insideUnitCircle.normalized * Random.Range(1f, maxSpawnRange);

            Vector3 spawnPosition = player.transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);

            float minSpawnDistance = 5f;
            float distanceToPlayer = Vector3.Distance(spawnPosition, player.transform.position);

            if (distanceToPlayer < minSpawnDistance)
            {
                spawnPosition += (spawnPosition - player.transform.position).normalized * minSpawnDistance;
            }

            Instantiate(enemyPrefab[randomIndex], spawnPosition, Quaternion.identity);
        }
    }

    void SpawnBoss5()
    {
        int randomBossIndex = Random.Range(0, bossPrefabs5.Length);
        Vector2 randomOffset = Random.insideUnitCircle.normalized * Random.Range(1f, maxSpawnRange);

        Vector3 spawnPosition = player.transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);

        float minSpawnDistance = 10f;
        float distanceToPlayer = Vector3.Distance(spawnPosition, player.transform.position);

        if (distanceToPlayer < minSpawnDistance)
        {
            spawnPosition += (spawnPosition - player.transform.position).normalized * minSpawnDistance;
        }

        Instantiate(bossPrefabs5[randomBossIndex], spawnPosition, Quaternion.identity);
    }

    void SpawnBoss10()
    {
        int randomBossIndex = Random.Range(0, bossPrefabs10.Length);
        Vector2 randomOffset = Random.insideUnitCircle.normalized * Random.Range(1f, maxSpawnRange);

        Vector3 spawnPosition = player.transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);

        float minSpawnDistance = 7f;
        float distanceToPlayer = Vector3.Distance(spawnPosition, player.transform.position);

        if (distanceToPlayer < minSpawnDistance)
        {
            spawnPosition += (spawnPosition - player.transform.position).normalized * minSpawnDistance;
        }

        Instantiate(bossPrefabs10[randomBossIndex], spawnPosition, Quaternion.identity);
    }
}
