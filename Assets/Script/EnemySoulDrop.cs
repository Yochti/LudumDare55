using UnityEngine;

public class EnemySoulDrop : MonoBehaviour
{
    public GameObject soulPrefab;     // Prefab pour les âmes
    public GameObject magnetPrefab;   // Prefab pour l'aimant
    public GameObject healthPrefab;   // Prefab pour le soin
    //public GameObject upgradePrefab;  // Prefab pour l'upgrade

    public int soulAmount = 1;        // Nombre d'âmes à faire apparaître

    // Taux de drop (en pourcentage)
    public float magnetDropChance = 0.5f;   // 0.5% de chance pour l'aimant
    public float healthDropChance = 1.5f;   // 1.5% de chance pour le soin
    public float upgradeDropChance = 0.5f;  // 0.5% de chance pour l'upgrade

    private void OnDestroy()
    {
        GenerateSouls();
        GenerateDrops();
    }

    void GenerateSouls()
    {
        for (int i = 0; i < soulAmount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            Vector3 soulPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
            Instantiate(soulPrefab, soulPosition, Quaternion.identity);
        }
    }

    void GenerateDrops()
    {
        if (Random.value * 100 < magnetDropChance)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            Vector3 dropPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
            Instantiate(magnetPrefab, dropPosition, Quaternion.identity);
        }

        if (Random.value * 100 < healthDropChance)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            Vector3 dropPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
            Instantiate(healthPrefab, dropPosition, Quaternion.identity);
        }

        /*if (Random.value * 100 < upgradeDropChance)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            Vector3 dropPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
            Instantiate(upgradePrefab, dropPosition, Quaternion.identity);
        }*/
    }
}
