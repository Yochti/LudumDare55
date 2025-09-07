using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ChestRarity
    {
        public string name;
        public GameObject prefab;
        [Range(0f, 100f)] public float spawnChance;
    }
    public saveSytem save;
    [Header("Liste des coffres par rareté")]
    public List<ChestRarity> chestRarities;

    [Header("Paramètres de spawn")]
    public int maxChests = 5;
    public Transform player;
    public Vector2 spawnSize = new Vector2(100f, 100f);

    private List<GameObject> activeChests = new List<GameObject>();

    void Start()
    {
        if (save.noChestDifficulty) return;
        for (int i = 0; i < maxChests; i++)
        {
            SpawnChest();
        }
    }

    public void OnChestCollected(GameObject chest)
    {
        activeChests.Remove(chest);
        SpawnChest();
    }

    void SpawnChest()
    {
        if (activeChests.Count >= maxChests || player == null) return;

        Vector2 center = player.position;

        Vector2 randomPosition = new Vector2(
            Random.Range(center.x - spawnSize.x / 2, center.x + spawnSize.x / 2),
            Random.Range(center.y - spawnSize.y / 2, center.y + spawnSize.y / 2)
        );

        GameObject selectedPrefab = GetRandomChestPrefab();
        if (selectedPrefab == null) return;

        GameObject newChest = Instantiate(selectedPrefab, randomPosition, Quaternion.identity);
        newChest.GetComponent<Chest>().SetSpawner(this);
        activeChests.Add(newChest);
    }

    GameObject GetRandomChestPrefab()
    {
        float total = 0f;
        foreach (var chest in chestRarities)
        {
            total += chest.spawnChance;
        }

        float randomPoint = Random.Range(0f, total);
        float current = 0f;

        foreach (var chest in chestRarities)
        {
            current += chest.spawnChance;
            if (randomPoint <= current)
            {
                return chest.prefab;
            }
        }

        return null;
    }

    private void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(player.position, spawnSize);
        }
    }
}
