using UnityEngine;
using System.Collections;
using TMPro;

public class EnemyWaveManager : MonoBehaviour
{
    public GameObject[] enemyPrefab; // Tableau des préfabriqués d'ennemis
    public float spawnInterval = 0.1f; // Intervalle entre chaque génération d'ennemi
    public float enemiesPerWave = 10f;
    public int wavesCount = 50; // Nombre de vagues total
    public float maxSpawnRange = 40f; // Portée maximale de génération
    public TextMeshProUGUI waveText;
    public GameObject player;

    private int currentWave = 0;
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
    }

    IEnumerator SpawnWave()
    {
        isWaveActive = true;

        
        SpawnEnemy();
        yield return new WaitForSeconds(spawnInterval);
        
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

        currentWave++;
        isWaveActive = false;

        if (currentWave < wavesCount)
        {
            waveText.gameObject.SetActive(true); 
            waveText.text = "Wave " + (currentWave + 1);

            yield return new WaitForSeconds(2f);
            waveText.gameObject.SetActive(false); 

            yield return new WaitForSeconds(3f);
            StartNextWave();
        }
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < enemiesPerWave + currentWave; i++)
        {
            int randomIndex = Random.Range(0, enemyPrefab.Length);
            Vector2 randomOffset = Random.insideUnitCircle.normalized * Random.Range(1f, maxSpawnRange);
            Vector3 spawnPosition = player.transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);

            Instantiate(enemyPrefab[randomIndex], spawnPosition, Quaternion.identity);
        }
    }
}
