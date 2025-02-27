using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class PowerUpPanel : MonoBehaviour
{
    public GameObject[] powerUpButtons;
    public GameObject panel;
    public static int numberRoll = 1;
    public powerUpBuy powerUpBuyScript;
    public TextMeshProUGUI numberRollText;


    private void Update()
    {
        numberRollText.text = numberRoll.ToString();
    }
    public void ShowRandomPowerUps()
    {
        if (!panel.activeSelf) return;

        foreach (GameObject button in powerUpButtons)
        {
            button.SetActive(false);
        }

        List<int> validIndices = GetValidPowerUpIndices();

        int[] randomIndices = GetRandomIndices(3, validIndices.Count);



        for (int i = 0; i < randomIndices.Length; i++)
        {
            int index = validIndices[randomIndices[i]];
            powerUpButtons[index].SetActive(true);
        }
    }
    public void ShowRandomPowerUpsButton()
    {

        if (numberRoll < 1) return;
        foreach (GameObject button in powerUpButtons)
        {
            button.SetActive(false);
        }

        List<int> validIndices = GetValidPowerUpIndices();

        int[] randomIndices = GetRandomIndices(3, validIndices.Count);



        for (int i = 0; i < randomIndices.Length; i++)
        {
            int index = validIndices[randomIndices[i]];
            powerUpButtons[index].SetActive(true);
        }
        numberRoll--;
    }

    private List<int> GetValidPowerUpIndices()
    {
        List<int> validIndices = new List<int>();

        // Vérifiez chaque bouton par rapport à son niveau actuel dans powerUpBuy
        if (powerUpBuyScript.lvlS < 4) validIndices.Add(0);  // Exemple pour Sickle
        if (powerUpBuyScript.lvlLa < 4) validIndices.Add(1); // Exemple pour Laser
        if (powerUpBuyScript.lvlP < 3) validIndices.Add(2);  // Exemple pour Poison
        if (powerUpBuyScript.lvlvAxes < 4) validIndices.Add(3); // Exemple pour Axes
        if (powerUpBuyScript.lvlvF < 4) validIndices.Add(4);         
        if (powerUpBuyScript.lvlBulletRain < 4) validIndices.Add(5);  


        return validIndices;
    }

    // Génération d'indices aléatoires
    private int[] GetRandomIndices(int count, int max)
    {
        int[] indices = new int[count];
        HashSet<int> selectedIndices = new HashSet<int>();

        for (int i = 0; i < count; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, max);
            } while (selectedIndices.Contains(randomIndex));

            selectedIndices.Add(randomIndex);
            indices[i] = randomIndex;
        }

        return indices;
    }
}
