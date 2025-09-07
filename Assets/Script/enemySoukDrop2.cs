using UnityEngine;

public class EnemySoukDrop2 : MonoBehaviour
{
    public GameObject soulPrefab;
    public GameObject soul2Prefab;
    public GameObject soul3Prefab;
    public GameObject magnetPrefab;
    public GameObject healthPrefab;
    public GameObject speedPrefab;
    private EnemyWaveManager waveManager;
    private saveSytem save;
    private staticRef refe;  
    public int soulAmount = 1;

    public float soul2Drop = 10f;
    public float soul3Drop = 0.75f;
    public float magnetDropChance = 0.3f;
    public float healthDropChance = 1.5f;
    public float speedBoostDropChance = 1f;

    [HideInInspector] public float upgradeDropChance;

    public float playerLuck = 0f;

    private void Start()
    {
        
        playerLuck = PlayerStats.luck;
        waveManager = FindObjectOfType<EnemyWaveManager>();
        save = FindObjectOfType<saveSytem>();
        if (waveManager == null )
        {
            Debug.LogError("EnemyWaveManager non trouvé dans la scène !");
        }
        if (save == null) print("pas de save sur le enemy souk drop");
        save.LoadData();
        // Recherche du script staticRef dans la scène
        refe = FindObjectOfType<staticRef>();

        if (refe == null)
        {
            Debug.LogError("staticRef non trouvé dans la scène !");
        }
    }



    private void OnDestroy()
    {
        upgradeDropChance = GetUpgradeDropChance();
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
        float luckMultiplier = 1f + (playerLuck / 100f);

        TryDropItem(magnetPrefab, magnetDropChance * luckMultiplier);
        TryDropItem(speedPrefab, speedBoostDropChance * luckMultiplier);
        TryDropItem(healthPrefab, healthDropChance * luckMultiplier);
        TryDropItem(soul2Prefab, soul2Drop * luckMultiplier);
        TryDropItem(soul3Prefab, soul3Drop * luckMultiplier * 1.3f);

        if (Random.value * 100 < upgradeDropChance * luckMultiplier)
        {
            if (refe != null)
            {
                refe.addPoint();
            }
            else
            {
                Debug.LogError("Impossible d'ajouter un point : staticRef est null !");
            }

            if (UIManager.instance != null && staticRef.pointUpgrade >= 3)
            {
                UIManager.instance.ShowPowerPanel();
                Time.timeScale = 0f;
                Cursor.visible = true;
                staticRef.pointUpgrade = 0;
            }
        }
    }

    void TryDropItem(GameObject itemPrefab, float dropChance)
    {
        if (Random.value * 100 < dropChance && !save.noDrop)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            Vector3 dropPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
            Instantiate(itemPrefab, dropPosition, Quaternion.identity);
        }
    }

    public float GetUpgradeDropChance()
    {
        float initialDropChance = 0.062f;
        float decayPerWave = 0.078f;

        int currentWave = waveManager != null ? waveManager.currentWave : 1;
        float upgradeDropChance = initialDropChance * Mathf.Exp(-decayPerWave * currentWave);

        if (currentWave >= 20)
        {
            upgradeDropChance = Mathf.Max(upgradeDropChance, 0.00075f);
        }

        return upgradeDropChance * 100f;
    }

}
