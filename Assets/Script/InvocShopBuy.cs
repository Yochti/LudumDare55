using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvocShopBuy : MonoBehaviour
{
    public saveSytem save;
    public int maxInvoc = 4; // Nombre maximum d'invocations sélectionnables
    public AudioSource audioo;  // Son d'achat réussi
    public AudioSource audioo2; // Son d'achat échoué
    public PurchaseConfirmationPanelInvocation confirmationPanel;  // Référence au panneau de confirmation

    // TextMeshProUGUI associés à chaque invocation pour afficher le numéro de sélection
    public TextMeshProUGUI textInvoc1;
    public TextMeshProUGUI textInvoc2;
    public TextMeshProUGUI textInvoc3;
    public TextMeshProUGUI textInvoc4;
    public TextMeshProUGUI textInvoc5;
    public TextMeshProUGUI textInvoc6;

    private List<string> selectedInvocs = new List<string>(); // Liste pour suivre les invocations sélectionnées

    private void Start()
    {
        ResetAllText(); // Initialise les textes
        save.LoadData(); // Charge les données sauvegardées
        LoadSelectedInvocations(); // Recharge les invocations sélectionnées précédemment
    }

    // Réinitialise tous les textes de sélection
    void ResetAllText()
    {
        textInvoc1.text = "";
        textInvoc2.text = "";
        textInvoc3.text = "";
        textInvoc4.text = "";
        textInvoc5.text = "";
        textInvoc6.text = "";
    }

    // Ajoute une invocation à la liste sélectionnée
    void AddInvocation(string invocName, TextMeshProUGUI invocText)
    {
        if (selectedInvocs.Count >= maxInvoc) return; // Limite le nombre de sélections

        selectedInvocs.Add(invocName); // Ajoute l'invocation à la liste
        UpdateInvocText(); // Met à jour les numéros affichés
        SaveSelectedInvocations(); // Sauvegarde l'ordre des invocations
    }

    // Supprime une invocation de la liste sélectionnée
    void RemoveInvocation(string invocName)
    {
        selectedInvocs.Remove(invocName); // Retire l'invocation de la liste
        UpdateInvocText(); // Met à jour les numéros affichés
        SaveSelectedInvocations(); // Sauvegarde l'ordre des invocations
    }

    // Vérifie si une invocation est déjà sélectionnée
    bool IsInvocSelected(string invocName)
    {
        return selectedInvocs.Contains(invocName); // Retourne true si l'invocation est déjà dans la liste
    }

    // Gère l'ajout ou le retrait de l'invocation lorsqu'on clique dessus
    void ToggleInvocation(string invocName, TextMeshProUGUI invocText)
    {
        if (IsInvocSelected(invocName))
        {
            RemoveInvocation(invocName);
        }
        else
        {
            AddInvocation(invocName, invocText);
        }
    }

    // Méthodes pour chaque invocation (à lier à chaque bouton)
    public void Invocation1()
    {
        ToggleInvocation("Bababoy", textInvoc1);
    }

    public void Invocation2()
    {
        ToggleInvocation("Name", textInvoc2);
    }

    public void Invocation3()
    {
        ToggleInvocation("Junktrap", textInvoc3);
    }

    public void Invocation4()
    {
        ToggleInvocation("Hermes", textInvoc4);
    }

    public void Invocation5()
    {
        TryPurchaseInvocation("Dimitri", 5000, textInvoc5);
    }

    public void Invocation6()
    {
        TryPurchaseInvocation("Hera", 5000, textInvoc6);
    }

    public void TryPurchaseInvocation(string invocName, int price, TextMeshProUGUI invocText)
    {
        // Vérifie si l'invocation est déjà possédée
        if (HasInvocation(invocName))
        {
            ToggleInvocation(invocName, invocText);
        }
        else
        {
            confirmationPanel.ShowConfirmation(invocName, price, this);
        }
    }

    // Confirmation d'achat (appelée par le panneau de confirmation)
    public void PurchaseInvocation(string invocName, int price)
    {
        if (save.TotalSouls >= price)
        {
            audioo.Play();  // Joue le son d'achat réussi
            save.TotalSouls -= price;
            AssignInvocationOwnership(invocName);
            save.SaveData();
        }
        else
        {
            audioo2.Play();  // Joue le son d'achat échoué
        }
    }

    private void AssignInvocationOwnership(string invocName)
    {
        switch (invocName)
        {
            case "Dimitri": save.hasInvoc5 = true; break;
            case "Hera": save.hasInvoc6 = true; break;
        }
    }

    private bool HasInvocation(string invocName)
    {
        switch (invocName)
        {
            case "Dimitri": return save.hasInvoc5;
            case "Hera": return save.hasInvoc6;
            default: return false;
        }
    }

    // Met à jour les textes des invocations et sauvegarde les sélections
    void UpdateInvocText()
    {
        ResetAllText(); // Réinitialise les textes avant de les réattribuer

        for (int i = 0; i < selectedInvocs.Count; i++)
        {
            string invocName = selectedInvocs[i];
            string number = (i + 1).ToString(); // Le numéro est l'index + 1

            // Réattribue les numéros en fonction des invocations sélectionnées
            if (invocName == "Bababoy") textInvoc1.text = number;
            else if (invocName == "Name") textInvoc2.text = number;
            else if (invocName == "Junktrap") textInvoc3.text = number;
            else if (invocName == "Hermes") textInvoc4.text = number;
            else if (invocName == "Dimitri") textInvoc5.text = number;
            else if (invocName == "Hera") textInvoc6.text = number;
        }
    }

    // Sauvegarde l'état des invocations sélectionnées
    void SaveSelectedInvocations()
    {
        save.InvocSlot1 = selectedInvocs.Count > 0 ? selectedInvocs[0] : "";
        save.InvocSlot2 = selectedInvocs.Count > 1 ? selectedInvocs[1] : "";
        save.InvocSlot3 = selectedInvocs.Count > 2 ? selectedInvocs[2] : "";
        save.InvocSlot4 = selectedInvocs.Count > 3 ? selectedInvocs[3] : "";

        save.SaveData(); // Appelle la fonction pour sauvegarder les données
    }

    // Recharge les invocations sélectionnées lors du chargement des données
    void LoadSelectedInvocations()
    {
        // Recharge les invocations des slots de sauvegarde
        if (!string.IsNullOrEmpty(save.InvocSlot1)) selectedInvocs.Add(save.InvocSlot1);
        if (!string.IsNullOrEmpty(save.InvocSlot2)) selectedInvocs.Add(save.InvocSlot2);
        if (!string.IsNullOrEmpty(save.InvocSlot3)) selectedInvocs.Add(save.InvocSlot3);
        if (!string.IsNullOrEmpty(save.InvocSlot4)) selectedInvocs.Add(save.InvocSlot4);

        UpdateInvocText(); // Met à jour l'affichage des textes après chargement
    }
}
