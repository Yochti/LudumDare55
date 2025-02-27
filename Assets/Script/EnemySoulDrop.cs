using UnityEngine;

public class EnemySoulDrop : MonoBehaviour
{
    public GameObject soulPrefab;     // Prefab pour les âmes
    public GameObject magnetPrefab;   // Prefab pour l'aimant
    public GameObject healthPrefab;   // Prefab pour le soin

    public int soulAmount = 1;        // Nombre d'âmes à faire apparaître

    // Taux de drop (en pourcentage)
    public float magnetDropChance = 0.3f;   // 0.5% de chance pour l'aimant
    public float healthDropChance = 1.5f;   // 1.5% de chance pour le soin
    [HideInInspector] public float upgradeDropChance;


    private void OnDestroy()
    {
        upgradeDropChance = GetUpgradeDropChance(staticRef.wavesS);

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

        if (Random.value * 100 < upgradeDropChance)
        {
            if (UIManager.instance != null)
            {
                UIManager.instance.ShowPowerPanel();
            }
            Time.timeScale = 0f;
            Cursor.visible = true;
        }
    }

    public float GetUpgradeDropChance(int currentWave)
    {
        float initialDropChance = 0.025f;

        float decayFactor = 0.18f;

        float upgradeDropChance = initialDropChance * Mathf.Exp(-decayFactor * currentWave);

        if (currentWave >= 20)
        {
            upgradeDropChance = Mathf.Max(upgradeDropChance, 0.0005f);
        }

        return upgradeDropChance *100;
    }
}
