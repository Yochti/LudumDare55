using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvocShopBuy : MonoBehaviour
{
    public saveSytem save;
    public int maxInvoc = 4; // Nombre maximum d'invocations s�lectionnables
    public AudioSource audioo;  // Son d'achat r�ussi
    public AudioSource audioo2; // Son d'achat �chou�
    public PurchaseConfirmationPanelInvocation confirmationPanel;  // R�f�rence au panneau de confirmation

    // TextMeshProUGUI associ�s � chaque invocation pour afficher le num�ro de s�lection
    public TextMeshProUGUI textInvoc1;
    public TextMeshProUGUI textInvoc2;
    public TextMeshProUGUI textInvoc3;
    public TextMeshProUGUI textInvoc4;
    public TextMeshProUGUI textInvoc5;
    public TextMeshProUGUI textInvoc6;

    private List<string> selectedInvocs = new List<string>(); // Liste pour suivre les invocations s�lectionn�es

    private void Start()
    {
        ResetAllText(); // Initialise les textes
        save.LoadData(); // Charge les donn�es sauvegard�es
        LoadSelectedInvocations(); // Recharge les invocations s�lectionn�es pr�c�demment
    }

    // R�initialise tous les textes de s�lection
    void ResetAllText()
    {
        textInvoc1.text = "";
        textInvoc2.text = "";
        textInvoc3.text = "";
        textInvoc4.text = "";
        textInvoc5.text = "";
        textInvoc6.text = "";
    }

    // Ajoute une invocation � la liste s�lectionn�e
    void AddInvocation(string invocName, TextMeshProUGUI invocText)
    {
        if (selectedInvocs.Count >= maxInvoc) return; // Limite le nombre de s�lections

        selectedInvocs.Add(invocName); // Ajoute l'invocation � la liste
        UpdateInvocText(); // Met � jour les num�ros affich�s
        SaveSelectedInvocations(); // Sauvegarde l'ordre des invocations
    }

    // Supprime une invocation de la liste s�lectionn�e
    void RemoveInvocation(string invocName)
    {
        selectedInvocs.Remove(invocName); // Retire l'invocation de la liste
        UpdateInvocText(); // Met � jour les num�ros affich�s
        SaveSelectedInvocations(); // Sauvegarde l'ordre des invocations
    }

    // V�rifie si une invocation est d�j� s�lectionn�e
    bool IsInvocSelected(string invocName)
    {
        return selectedInvocs.Contains(invocName); // Retourne true si l'invocation est d�j� dans la liste
    }

    // G�re l'ajout ou le retrait de l'invocation lorsqu'on clique dessus
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

    // M�thodes pour chaque invocation (� lier � chaque bouton)
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
        // V�rifie si l'invocation est d�j� poss�d�e
        if (HasInvocation(invocName))
        {
            ToggleInvocation(invocName, invocText);
        }
        else
        {
            confirmationPanel.ShowConfirmation(invocName, price, this);
        }
    }

    // Confirmation d'achat (appel�e par le panneau de confirmation)
    public void PurchaseInvocation(string invocName, int price)
    {
        if (save.TotalSouls >= price)
        {
            audioo.Play();  // Joue le son d'achat r�ussi
            save.TotalSouls -= price;
            AssignInvocationOwnership(invocName);
            save.SaveData();
        }
        else
        {
            audioo2.Play();  // Joue le son d'achat �chou�
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

    // Met � jour les textes des invocations et sauvegarde les s�lections
    void UpdateInvocText()
    {
        ResetAllText(); // R�initialise les textes avant de les r�attribuer

        for (int i = 0; i < selectedInvocs.Count; i++)
        {
            string invocName = selectedInvocs[i];
            string number = (i + 1).ToString(); // Le num�ro est l'index + 1

            // R�attribue les num�ros en fonction des invocations s�lectionn�es
            if (invocName == "Bababoy") textInvoc1.text = number;
            else if (invocName == "Name") textInvoc2.text = number;
            else if (invocName == "Junktrap") textInvoc3.text = number;
            else if (invocName == "Hermes") textInvoc4.text = number;
            else if (invocName == "Dimitri") textInvoc5.text = number;
            else if (invocName == "Hera") textInvoc6.text = number;
        }
    }

    // Sauvegarde l'�tat des invocations s�lectionn�es
    void SaveSelectedInvocations()
    {
        save.InvocSlot1 = selectedInvocs.Count > 0 ? selectedInvocs[0] : "";
        save.InvocSlot2 = selectedInvocs.Count > 1 ? selectedInvocs[1] : "";
        save.InvocSlot3 = selectedInvocs.Count > 2 ? selectedInvocs[2] : "";
        save.InvocSlot4 = selectedInvocs.Count > 3 ? selectedInvocs[3] : "";

        save.SaveData(); // Appelle la fonction pour sauvegarder les donn�es
    }

    // Recharge les invocations s�lectionn�es lors du chargement des donn�es
    void LoadSelectedInvocations()
    {
        // Recharge les invocations des slots de sauvegarde
        if (!string.IsNullOrEmpty(save.InvocSlot1)) selectedInvocs.Add(save.InvocSlot1);
        if (!string.IsNullOrEmpty(save.InvocSlot2)) selectedInvocs.Add(save.InvocSlot2);
        if (!string.IsNullOrEmpty(save.InvocSlot3)) selectedInvocs.Add(save.InvocSlot3);
        if (!string.IsNullOrEmpty(save.InvocSlot4)) selectedInvocs.Add(save.InvocSlot4);

        UpdateInvocText(); // Met � jour l'affichage des textes apr�s chargement
    }
}
