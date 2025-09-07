using UnityEngine;
using System.Collections;
using TMPro;

public class EnemySpawnTime : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] bossPrefabs5;
    public GameObject[] bossPrefabs10;
    public float spawnInterval = 0.05f;
    public float baseEnemiesPerMinute = 5f;
    public float difficultyMultiplier = 3.5f;
    public float maxSpawnRange = 150f;
    public TextMeshProUGUI timerText;
    public GameObject player;
    public GameObject winPanel;

    private float elapsedTime = 0f;
    private float enemySpawnRate;
    private bool isGameActive = true;

    public EnemmiHealth[] eHp;
    public Boss1Health[] bHp;
    public Boss5Behaviour boss5AI;
    public EnemyAI eAI1;
    public BulletLifEnemy bulletEnemy;
    public EnemyAI3 eAI3;
    public EnemyController eAI5;
    public Enemy7AI eAI7;
    public Enemy8AI eAI8;
    public Enemy9AI eAI9;
    public MirageEnemy eMirageAI;
    public LargeGolemAI eBigAI;

    void Start()
    {
        SetInitialEnemyStats();
        StartCoroutine(SpawnEnemiesOverTime());
        StartCoroutine(CheckForBossSpawns());
    }

    void Update()
    {
        if (isGameActive)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    void SetInitialEnemyStats()
    {
        for (int i = 0; i < eHp.Length; i++)
            eHp[i].maxHealth = 50;

        eHp[11].maxHealth = 25;
        eHp[0].maxHealth = 100;

        for (int y = 0; y < bHp.Length; y++)
            bHp[y].maxHealth = 2500;

        bHp[3].maxHealth = 1000;
        boss5AI.numberOfSmallEnemies = 20;

        eAI1.damage = 10;
        bulletEnemy.bulletDamage = 8f;
        eAI3.explosionDamage = 30f;
        eAI3.moveSpeed = 5.5f;
        eAI5.attackDamage = 14;
        eAI7.damage = 12;
        eAI8.damage = 12;
        eAI8.moveSpeed = 4.5f;
        eAI9.attackDamage = 12;
        eMirageAI.attackDamage = 16;
        eMirageAI.maxIllusions = 4;
        eBigAI.damage = 16;
    }

    IEnumerator SpawnEnemiesOverTime()
    {
        while (isGameActive)
        {
            SpawnEnemies();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator CheckForBossSpawns()
    {
        while (isGameActive)
        {
            int elapsedMinutes = Mathf.FloorToInt(elapsedTime / 60f);

            if (elapsedMinutes % 10 == 0 && elapsedMinutes > 0)
            {
                SpawnBoss10();
                IncreaseDifficulty();
            }
            else if (elapsedMinutes % 5 == 0 && elapsedMinutes > 0)
            {
                SpawnBoss5();
                IncreaseDifficulty();
            }

            yield return new WaitForSeconds(60f); // Vérifie une fois par minute
        }
    }

    void SpawnEnemies()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogError("Le tableau enemyPrefabs est vide ou non assigné !");
            return;
        }

        int enemiesToSpawn = Mathf.FloorToInt(baseEnemiesPerMinute + (elapsedTime / 60f * difficultyMultiplier));
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            Vector2 randomOffset = Random.insideUnitCircle.normalized * Random.Range(1f, maxSpawnRange);

            Vector3 spawnPosition = player.transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
            float minSpawnDistance = 15f;

            if (Vector3.Distance(spawnPosition, player.transform.position) < minSpawnDistance)
            {
                spawnPosition += (spawnPosition - player.transform.position).normalized * minSpawnDistance;
            }

            Instantiate(enemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        }
    }

    void SpawnBoss5()
    {
        int randomBossIndex = Random.Range(0, bossPrefabs5.Length);
        Vector2 randomOffset = Random.insideUnitCircle.normalized * Random.Range(1f, maxSpawnRange);

        Vector3 spawnPosition = player.transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
        spawnPosition = AdjustSpawnPosition(spawnPosition, 10f);

        Instantiate(bossPrefabs5[randomBossIndex], spawnPosition, Quaternion.identity);
    }

    void SpawnBoss10()
    {
        int randomBossIndex = Random.Range(0, bossPrefabs10.Length);
        Vector2 randomOffset = Random.insideUnitCircle.normalized * Random.Range(1f, maxSpawnRange);

        Vector3 spawnPosition = player.transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
        spawnPosition = AdjustSpawnPosition(spawnPosition, 7f);

        Instantiate(bossPrefabs10[randomBossIndex], spawnPosition, Quaternion.identity);
    }

    Vector3 AdjustSpawnPosition(Vector3 spawnPosition, float minDistance)
    {
        float distanceToPlayer = Vector3.Distance(spawnPosition, player.transform.position);

        if (distanceToPlayer < minDistance)
        {
            spawnPosition += (spawnPosition - player.transform.position).normalized * minDistance;
        }

        return spawnPosition;
    }

    void IncreaseDifficulty()
    {
        for (int i = 0; i < eHp.Length; i++)
        {
            eHp[i].maxHealth += 20;
        }

        for (int y = 0; y < bHp.Length; y++)
        {
            bHp[y].maxHealth += 1000;
        }

        boss5AI.numberOfSmallEnemies += 13;
        eAI1.damage += 8f;
        eAI1.moveSpeed += .2f;
        eAI3.explosionDamage += 10f;
        eAI3.moveSpeed += 0.5f;
        eAI7.damage += 10f;
        eAI8.damage += 10;
        eAI8.moveSpeed += .25f;
        eMirageAI.maxIllusions += 1;
        eMirageAI.attackDamage += 4;
        eAI9.attackDamage += 4f;
        eAI5.attackDamage += 5;
        eBigAI.damage += 10;
        bulletEnemy.bulletDamage += 7f;
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
}
