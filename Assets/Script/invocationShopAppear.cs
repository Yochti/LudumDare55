using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocationShopAppear : MonoBehaviour
{
    public saveSytem save;

    public GameObject[] invocPanels; // Array de panels pour les invocations

    private Dictionary<string, GameObject> invocMapping;

    private void OnEnable()
    {
        save.LoadData();

        List<string> allInvocations = new List<string>
        {
            save.InvocSlot1,
            save.InvocSlot2,
            save.InvocSlot3,
            save.InvocSlot4
        };

        // Vérifie s’il n’y a aucune invocation active
        bool allEmpty = allInvocations.TrueForAll(string.IsNullOrEmpty);
        if (allEmpty)
        {
            gameObject.SetActive(false); // Désactive complètement le panel
            return;
        }

        Time.timeScale = 0f;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        invocMapping = new Dictionary<string, GameObject>
        {
            { "Bababoy", invocPanels[0] },
            { "Name", invocPanels[1] },
            { "Junktrap", invocPanels[2] },
            { "Hermes", invocPanels[3] },
            { "Dimitri", invocPanels[4] },
            { "Hera", invocPanels[5] },
            { "Izo", invocPanels[6] }
        };

        ShowRandomInvocations(allInvocations);
    }

    private void ShowRandomInvocations(List<string> allInvocations)
    {
        HashSet<int> selectedIndices = new HashSet<int>();

        while (selectedIndices.Count < 3 && selectedIndices.Count < allInvocations.Count)
        {
            int randomIndex = Random.Range(0, allInvocations.Count);
            if (!string.IsNullOrEmpty(allInvocations[randomIndex]))
            {
                selectedIndices.Add(randomIndex);
            }
        }

        foreach (int index in selectedIndices)
        {
            string invocName = allInvocations[index];
            if (invocMapping.ContainsKey(invocName))
            {
                GameObject invocPanel = invocMapping[invocName];
                invocPanel.SetActive(true);
            }
        }
    }
}
