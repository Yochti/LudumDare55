using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
public class PowerUpWaves : MonoBehaviour
{
    [System.Serializable]
    public class PowerUp
    {
        public string name;
        public Rarity rarity;
        public GameObject gameObject;
        public bool removeAfterChoice;    }
    public MouseCursor cursor;
    public static PowerUpWaves Instance;
    public bool overrideRarityFromChest = false;
    public GameObject[] hideUI;
    public static int rerollCount = 0;
    private bool rerollUsedThisPanel = false;
    public TextMeshProUGUI rerollText;
    public saveSytem save;
    public static bool rerollStatus;
    public enum Rarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    public List<PowerUp> powerUps; // Tous les power-ups disponibles
    public float playerLuck;       // Chance du joueur
    public Rarity currentRarity;   // Définie par le coffre qui l'ouvre

    private Dictionary<Rarity, float> rarityBaseProbabilities = new Dictionary<Rarity, float>()
    {
        { Rarity.Common, 80f },
        { Rarity.Rare, 15f },
        { Rarity.Epic, 4.5f },
        { Rarity.Legendary, 0.5f }
    };

    private HashSet<PowerUp> activePowerUps = new HashSet<PowerUp>();
    private void Awake()
    {
        rerollStatus = save.rerollDifficulty;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }
        Instance = this;
        rerollCount = 0;
        this.gameObject.SetActive(false);
    }
    void OnEnable()
    {
        Cursor.visible = true;
        for (int i = 0; i < hideUI.Length; i++)
        {
            hideUI[i].gameObject.SetActive(false);
        }

        Time.timeScale = 0f;
        
        if (!rerollUsedThisPanel && !save.rerollDifficulty)
            rerollCount++;
        if (rerollCount < 0) rerollCount = 0;

        ActivateRandomPowerUps();
    }
   


    public void RemoveChosenPowerUp(string powerUpName)
    {
        PowerUp toRemove = powerUps.Find(p => p.name == powerUpName && p.removeAfterChoice);
        if (toRemove != null)
        {
            powerUps.Remove(toRemove);
        }
    }

    void OnDisable()
    {

        for (int i = 0; i < hideUI.Length; i++)
        {
            hideUI[i].gameObject.SetActive(true);
        }
        DesactivateRandomPowerUps();
        rerollUsedThisPanel = false;
        overrideRarityFromChest = false;


    }

    void Update()
    {
        rerollText.text = "Rerolls: " + rerollCount;
        playerLuck = PlayerStats.luck;
    }
    public void RerollPowerUps()
    {
        if (rerollCount <= 0)
        {
            Debug.Log("Aucun reroll disponible.");
            return;
        }

        rerollCount--;
        rerollUsedThisPanel = true;

        DesactivateRandomPowerUps();
        ActivateRandomPowerUps();
    }


    void ActivateRandomPowerUps()
    {
        if (powerUps == null || powerUps.Count == 0)
        {
            Debug.LogError("La liste des power-ups est vide !");
            return;
        }

        List<KeyValuePair<PowerUp, float>> adjustedProbabilities = AdjustProbabilitiesByLuck();

        if (adjustedProbabilities.Count == 0)
        {
            Debug.LogWarning("Aucun power-up disponible pour la rareté actuelle !");
            return;
        }

        HashSet<PowerUp> selectedPowerUps = new HashSet<PowerUp>();
        List<Rarity> selectedRarities = new List<Rarity>();

        while (selectedPowerUps.Count < 3 && selectedPowerUps.Count < adjustedProbabilities.Count)
        {
            PowerUp selected = GetRandomPowerUp(adjustedProbabilities);
            if (selected != null && !selectedPowerUps.Contains(selected))
            {
                selectedPowerUps.Add(selected);
                selected.gameObject.SetActive(true);
                activePowerUps.Add(selected);
                selectedRarities.Add(selected.rarity);
            }
        }

        
    }

    void DesactivateRandomPowerUps()
    {
        foreach (PowerUp powerUp in activePowerUps)
        {
            if (powerUp != null)
                powerUp.gameObject.SetActive(false);
        }

        activePowerUps.Clear();
    }

    List<KeyValuePair<PowerUp, float>> AdjustProbabilitiesByLuck()
    {
        List<KeyValuePair<PowerUp, float>> adjusted = new List<KeyValuePair<PowerUp, float>>();

        foreach (PowerUp powerUp in powerUps)
        {
            if (overrideRarityFromChest)
            {
                if (powerUp.rarity != currentRarity) continue;
            }

            float baseProbability = rarityBaseProbabilities[powerUp.rarity];
            float adjustedProbability = AdjustProbabilityForLuck(baseProbability, powerUp.rarity);
            adjusted.Add(new KeyValuePair<PowerUp, float>(powerUp, adjustedProbability));
        }
        return adjusted;
    }

    float AdjustProbabilityForLuck(float baseProbability, Rarity rarity)
    {
        float multiplier = 1f + (playerLuck / 100f);

        switch (rarity)
        {
            case Rarity.Rare: return baseProbability * multiplier * 1.15f;
            case Rarity.Epic: return baseProbability * multiplier * 1.8f;
            case Rarity.Legendary: return baseProbability * multiplier * 2.5f;
            default: return baseProbability;
        }
    }

    PowerUp GetRandomPowerUp(List<KeyValuePair<PowerUp, float>> probabilities)
    {
        float totalWeight = 0f;

        foreach (var entry in probabilities)
            totalWeight += entry.Value;

        float randomPoint = Random.Range(0f, totalWeight);
        float currentWeight = 0f;

        foreach (var entry in probabilities)
        {
            currentWeight += entry.Value;
            if (randomPoint <= currentWeight)
                return entry.Key;
        }

        return null;
    }
}
