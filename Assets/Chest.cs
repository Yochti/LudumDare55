using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Rareté du coffre")]
    public PowerUpWaves.Rarity rarity;

    private ChestSpawner spawner;
    private PowerUpWaves powerUpManager;
    private bool isOpened = false;

    private void Start()
    {
        powerUpManager = PowerUpWaves.Instance;
        if (powerUpManager == null)
        {
            Debug.LogWarning("[CHEST] Aucun PowerUpWaves trouvé dans la scène !");
        }
    }


    public void SetSpawner(ChestSpawner spawner)
    {
        this.spawner = spawner;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpened || !other.CompareTag("Player")) return;

        isOpened = true;
        Debug.Log("[CHEST] Collision avec Player détectée. Ouverture du coffre...");
        OpenChest();
    }

    void OpenChest()
    {
        if (powerUpManager != null)
        {
            Debug.Log($"[CHEST] Activation du panel de stats pour rareté : {rarity}");
            powerUpManager.overrideRarityFromChest = true;
            powerUpManager.currentRarity = rarity;
            if (!PowerUpWaves.rerollStatus) PowerUpWaves.rerollCount--;
            powerUpManager.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("[CHEST] PowerUpManager est nul, impossible d'activer le panel !");
        }

        if (spawner != null)
        {
            spawner.OnChestCollected(gameObject);
        }

        Destroy(gameObject);
    }
}
