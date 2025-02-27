using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Special : MonoBehaviour
{
    public saveSytem save;
    public Button special1sprite;
    public Button special2sprite;
    public Button special3sprite;
    public Color baseColor;
    public AudioSource audioo;
    public PurchaseConfirmationPanelSpecial confirmationPanel;
    public AudioSource audioo2;

    // Dictionnaire pour gérer l'état de possession de chaque spécial
    private Dictionary<string, bool> ownedSpecials;

    private void Start()
    {
        // Initialisation du dictionnaire
        ownedSpecials = new Dictionary<string, bool>
        {
            { "Special1", true },  // Special1 est déjà possédé
            { "Special2", save.hasSpecial2 },
            { "Special3", save.hasSpecial3 }
        };
    }

    private void Update()
    {
        UpdateSpecialColors();
    }

    private void UpdateSpecialColors()
    {
        // Applique la couleur de sélection sur le spécial actuellement équipé
        special1sprite.GetComponent<Image>().color = save.whichSpecial == "Special1" ? Color.green : baseColor;
        special2sprite.GetComponent<Image>().color = save.whichSpecial == "Special2" ? Color.green : baseColor;
        special3sprite.GetComponent<Image>().color = save.whichSpecial == "Special3" ? Color.green : baseColor;
    }

    public void Special1()
    {
        save.whichSpecial = "Special1";
        save.SaveData();
    }

    public void Special2()
    {
        if (ownedSpecials["Special2"])
        {
            save.whichSpecial = "Special2";
            save.SaveData();
        }
        else
        {
            confirmationPanel.ShowConfirmation("Special2", 1000, this);
        }
    }

    public void Special3()
    {
        if (ownedSpecials["Special3"])
        {
            save.whichSpecial = "Special3";
            save.SaveData();
        }
        else
        {
            confirmationPanel.ShowConfirmation("Special3", 1000, this);
        }
    }

    public void PurchaseSpecial(string specialName, int price)
    {
        if (save.TotalSouls >= price)
        {
            save.TotalSouls -= price;
            save.whichSpecial = specialName;
            audioo.Play();

            // Marquer le spécial comme possédé et sauvegarder
            ownedSpecials[specialName] = true;
            UpdateSaveData(specialName);

            save.SaveData();
        }
        else
        {
            audioo2.Play();
        }
    }

    private void UpdateSaveData(string specialName)
    {
        if (specialName == "Special2")
        {
            save.hasSpecial2 = true;
        }
        else if (specialName == "Special3")
        {
            save.hasSpecial3 = true;
        }
    }
}
