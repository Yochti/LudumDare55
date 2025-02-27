using UnityEngine;

public class EnemySoukDrop2 : MonoBehaviour
{
    public GameObject soulPrefab;    
    public GameObject soul2Prefab;    
    public GameObject soul3Prefab;   
    public GameObject magnetPrefab;   // Prefab pour l'aimant
    public GameObject healthPrefab;
    public GameObject speedPrefab;
    public int soulAmount = 1;

    // Taux de drop (en pourcentage)
    public float soul2Drop = 10f;
    public float soul3Drop = 0.75f;
    public float magnetDropChance = 0.3f;   
    public float healthDropChance = 1.5f;
    public float speedBoostDropChance = 1f; 
    [HideInInspector] public float upgradeDropChance;

    private float timeElapsed; // Temps écoulé depuis le début du jeu

    public float playerLuck = 0f; // Valeur de chance du joueur (par défaut 0)

    private void Start()
    {
        timeElapsed = 0f;
        playerLuck = PlayerStats.luck; // Récupère la chance du joueur
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    private void OnDestroy()
    {
        upgradeDropChance = GetUpgradeDropChance(timeElapsed);

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
        // Augmente les chances de drop en fonction de la chance du joueur
        float luckMultiplier = 1f + (playerLuck / 100f);

        if (Random.value * 100 < magnetDropChance * luckMultiplier)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            Vector3 dropPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
            Instantiate(magnetPrefab, dropPosition, Quaternion.identity);
        }
        if (Random.value * 100 < speedBoostDropChance * luckMultiplier)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            Vector3 dropPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
            Instantiate(speedPrefab, dropPosition, Quaternion.identity);
        }

        if (Random.value * 100 < healthDropChance * luckMultiplier)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            Vector3 dropPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
            Instantiate(healthPrefab, dropPosition, Quaternion.identity);
        }

        if (Random.value * 100 < upgradeDropChance * luckMultiplier)
        {
            if (UIManager.instance != null)
            {
                UIManager.instance.ShowPowerPanel();
            }
            Time.timeScale = 0f;
            Cursor.visible = true;
        }
        if(Random.value * 100<soul2Drop * luckMultiplier)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            Vector3 soulPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
            Instantiate(soul2Prefab, soulPosition, Quaternion.identity);
        }if(Random.value * 100<soul3Drop * luckMultiplier * 1.3f)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            Vector3 soulPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
            Instantiate(soul3Prefab, soulPosition, Quaternion.identity);
        }
    }

    public float GetUpgradeDropChance(float timeElapsed)
    {
        float initialDropChance = 0.014f;
        float decayFactor = 0.16f;

        float timeInMinutes = timeElapsed / 60f;

        float upgradeDropChance = initialDropChance * Mathf.Exp(-decayFactor * timeInMinutes);

        if (timeInMinutes >= 20)
        {
            upgradeDropChance = Mathf.Max(upgradeDropChance, 0.0005f);
        }

        return upgradeDropChance * 100;
    }
}
