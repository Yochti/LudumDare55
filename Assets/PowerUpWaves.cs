using System.Collections.Generic;
using UnityEngine;

public class PowerUpWaves : MonoBehaviour
{
    [System.Serializable]
    public class PowerUp
    {
        public string name;
        public Rarity rarity;
        public GameObject gameObject; // L'objet à activer
    }

    public enum Rarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    public List<PowerUp> powerUps; // Liste des power-ups
    public float playerLuck; // Valeur de la chance du joueur

    private Dictionary<Rarity, float> rarityBaseProbabilities = new Dictionary<Rarity, float>()
    {
        { Rarity.Common, 80f },
        { Rarity.Rare, 15f },
        { Rarity.Epic, 4.5f },
        { Rarity.Legendary, 0.5f }
    };

    private HashSet<PowerUp> activePowerUps = new HashSet<PowerUp>(); // Pour garder une trace des power-ups activés

    void OnEnable()
    {
        Time.timeScale = 0f;
        ActivateRandomPowerUps();
    }

    private void OnDisable()
    {
        DesactivateRandomPowerUps();
    }

    private void Update()
    {
        playerLuck = PlayerStats.luck;

    }

    void ActivateRandomPowerUps()
    {
        if (powerUps == null || powerUps.Count == 0)
        {
            Debug.LogError("La liste des power-ups est vide !");
            return;
        }

        // Calcule les probabilités ajustées pour chaque power-up
        List<KeyValuePair<PowerUp, float>> adjustedProbabilities = AdjustProbabilitiesByLuck();

        // Sélectionne et active 3 power-ups uniques
        HashSet<PowerUp> selectedPowerUps = new HashSet<PowerUp>();
        while (selectedPowerUps.Count < 3 && selectedPowerUps.Count < powerUps.Count)
        {
            PowerUp selected = GetRandomPowerUp(adjustedProbabilities);
            if (selected != null)
            {
                selectedPowerUps.Add(selected);
                selected.gameObject.SetActive(true);
                activePowerUps.Add(selected); // Ajouter à la liste des power-ups actifs
            }
        }

        // Debug des résultats
        foreach (PowerUp powerUp in selectedPowerUps)
        {
            Debug.Log($"Activated PowerUp: {powerUp.name}");
        }
    }

    void DesactivateRandomPowerUps()
    {
        if (activePowerUps == null || activePowerUps.Count == 0)
        {
            return;
        }

        // Désactive tous les power-ups actifs
        foreach (PowerUp powerUp in activePowerUps)
        {
            if (powerUp != null)
            {
                powerUp.gameObject.SetActive(false);
            }
        }

        activePowerUps.Clear(); // Vide la liste des power-ups actifs après les avoir désactivés
    }

    List<KeyValuePair<PowerUp, float>> AdjustProbabilitiesByLuck()
    {
        List<KeyValuePair<PowerUp, float>> adjusted = new List<KeyValuePair<PowerUp, float>>();

        // Ajuste les probabilités par rareté en fonction de la chance
        foreach (PowerUp powerUp in powerUps)
        {
            float baseProbability = rarityBaseProbabilities[powerUp.rarity];
            float adjustedProbability = AdjustProbabilityForLuck(baseProbability, powerUp.rarity);
            adjusted.Add(new KeyValuePair<PowerUp, float>(powerUp, adjustedProbability));
        }

        return adjusted;
    }

    float AdjustProbabilityForLuck(float baseProbability, Rarity rarity)
    {
        float multiplier = 1f + (playerLuck / 100f);

        if (rarity == Rarity.Rare) return baseProbability * multiplier * 1.15f;
        if (rarity == Rarity.Epic) return baseProbability * multiplier * 1.8f;
        if (rarity == Rarity.Legendary) return baseProbability * multiplier * 2.5f;

        return baseProbability; // Common
    }

    PowerUp GetRandomPowerUp(List<KeyValuePair<PowerUp, float>> probabilities)
    {
        float totalWeight = 0f;

        // Calcule le poids total
        foreach (var entry in probabilities)
        {
            totalWeight += entry.Value;
        }

        float randomPoint = Random.Range(0f, totalWeight);
        float currentWeight = 0f;

        // Sélectionne un élément aléatoirement basé sur les poids
        foreach (var entry in probabilities)
        {
            currentWeight += entry.Value;
            if (randomPoint <= currentWeight)
            {
                return entry.Key;
            }
        }

        Debug.LogWarning("Aucun power-up sélectionné, retourne un power-up par défaut.");
        return null;
    }
}
