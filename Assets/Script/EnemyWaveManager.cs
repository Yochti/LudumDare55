using UnityEngine;
using System.Collections;
using TMPro;

public class EnemyWaveManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] bossPrefabs5; 
    public GameObject[] bossPrefabs10; 
    public float spawnInterval = 0.05f;
    public float enemiesPerWave = 5f;
    public float multiplicator = 3.5f;
    public int wavesCount = 50; 
    public float maxSpawnRange = 150f;
    public TextMeshProUGUI waveText;
    public GameObject player;
    // Script enemy
    public EnemmiHealth[] eHp;
    public Boss1Health[] bHp;
    public PlayerHealth pH;
    public Boss5Behaviour boss4AI;
    public EnemyAI eAI1;
    public BulletLifEnemy bulletEnemy;
    public EnemyAI3 eAI3;
    public EnemyController eAI5;
    public Enemy7AI eAI7;
    public Enemy8AI eAI8;
    public Enemy9AI eAI9;
    public MirageEnemy eMirageAI;
    public LargeGolemAI eBigAI;
    public static bool hasRegenKillGolem;
    public static bool hasSpeedBoostWaveActivated;
    public GameObject powerUpPanel;
    public int currentWave = 0;
    private bool isWaveActive = false;
    public GameObject winPanel;
    public saveSytem save;
    void Start()
    {
        for (int i = 0; i < eHp.Length; i++)
        {
            eHp[i].maxHealth = 95;
        }
        eHp[11].maxHealth = 45;
        eHp[0].maxHealth = 200;

        for (int y = 0; y < bHp.Length; y++)
        {
            bHp[y].maxHealth = 2100;
        }
        bHp[3].maxHealth = 1350;
        bHp[2].maxHealth = 1600;

        // Valeurs de base des ennemis
        eAI1.damage = 10;
        bulletEnemy.bulletDamage = 8f;
        eAI3.explosionDamage = 40f;
        eAI3.moveSpeed = 8f;
        eAI5.attackDamage = 14;
        eAI7.damage = 11;
        eAI8.damage = 12;
        eAI8.moveSpeed = 5.5f;
        eAI9.attackDamage = 12;
        eMirageAI.attackDamage = 16;
        eMirageAI.maxIllusions = 4;
        eBigAI.damage = 16;

        if (save.generalDifficulty)
        {
            for (int i = 0; i < eHp.Length; i++)
            {
                eHp[i].maxHealth += 45; 
            }

            for (int y = 0; y < bHp.Length; y++)
            {
                bHp[y].maxHealth += 400; 
            }

            eAI1.damage += 4f;
            bulletEnemy.bulletDamage += 3f;
            eAI3.explosionDamage += 10f;
            eAI3.moveSpeed += 0.5f;
            eAI5.attackDamage += 5;
            eAI7.damage += 4;
            eAI8.damage += 4;
            eAI8.moveSpeed += 0.5f;
            eAI9.attackDamage += 4;
            eMirageAI.attackDamage += 5;
            eMirageAI.maxIllusions += 1;
            eBigAI.damage += 6;
        }
        if (save.enemyAmountDifficulty) multiplicator = 5f;
        else multiplicator = 3.5f;
        StartCoroutine(InitialDelay());
    }

    IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(5f);
        StartNextWave();
    }

    void StartNextWave()
    {
        if(hasRegenKillGolem) pH.currentHealth = (int)(PlayerHealth.maxHealth * 0.15f);
        StartCoroutine(SpawnWave());

        if ((currentWave + 1) == 10 || (currentWave > 0 && (currentWave + 1) % 10 == 0))
        {
            SpawnBoss10();
            IncreaseEnemyStats5();
            IncreaseEnemyStats10();

        }
        else if ((currentWave + 1) == 5 || (currentWave > 0 && (currentWave + 1) % 5 == 0))
        {
            SpawnBoss5();
            IncreaseEnemyStats10();
        }

    }

    IEnumerator SpawnWave()
    {
        
        
        isWaveActive = true;

        SpawnEnemy();
        if (hasSpeedBoostWaveActivated)
        {
            PlayerStats.moveSpeed += 4f;
            StartCoroutine(ReduceMoveSpeedGradually(4f, 12f));
        }
        yield return new WaitForSeconds(spawnInterval);

        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Boss").Length == 0);
        isWaveActive = false;
        currentWave++;
        if (currentWave < wavesCount)
        {
            
            yield return new WaitForSeconds(0.8f);
            pH.Heal(PlayerStats.healthRegenWaves);
            powerUpPanel.SetActive(true);
            waveText.text = "Wave " + (currentWave + 1);

            yield return new WaitForSeconds(0.01f);
            yield return new WaitForSeconds(1f);
            StartNextWave();
        }
        else
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
        }
    }
    private IEnumerator ReduceMoveSpeedGradually(float totalReduction, float duration)
    {
        float elapsed = 0f;
        float stepTime = 0.5f;
        int steps = Mathf.RoundToInt(duration / stepTime);
        float speedStep = totalReduction / steps;

        while (elapsed < duration)
        {
            yield return new WaitForSeconds(stepTime);
            PlayerStats.moveSpeed -= speedStep;
            elapsed += stepTime;
        }

        PlayerStats.moveSpeed = Mathf.Max(0, PlayerStats.moveSpeed);
    }

    void IncreaseEnemyStats5()
    {
        for (int i = 0; i < eHp.Length; i++)
        {
            eHp[i].maxHealth += 20;
        }
    }
    void IncreaseEnemyStats10()
    {
        for (int i = 0; i < eHp.Length; i++)
        {
            eHp[i].maxHealth += 20;
        }
        for (int y = 0; y < bHp.Length; y++)
        {
            bHp[y].maxHealth += 650;
        }
        boss4AI.numberOfSmallEnemies += 20;
        eAI1.damage += 6f;
        eAI1.moveSpeed += .2f;
        eAI3.explosionDamage += 10f;
        eAI3.moveSpeed += 0.5f;
        eAI7.damage += 8f;
        eAI8.damage += 8;
        eAI8.moveSpeed += .25f;
        eMirageAI.maxIllusions += 1;
        eMirageAI.attackDamage += 4;
        eAI9.attackDamage += 4f;
        eAI5.attackDamage += 5;
        eBigAI.damage += 8;
        bulletEnemy.bulletDamage += 7f;
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < enemiesPerWave + (currentWave * multiplicator); i++)
        {
            int randomIndex = Random.Range(0, enemyPrefab.Length);
            Vector2 randomOffset = Random.insideUnitCircle.normalized * Random.Range(1f, maxSpawnRange);

            Vector3 spawnPosition = player.transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);

            float minSpawnDistance = 15f;
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
