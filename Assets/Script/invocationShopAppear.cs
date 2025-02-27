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
            { "Hera", invocPanels[5] }
        };

        save.LoadData();

        if (string.IsNullOrEmpty(save.InvocSlot1))
            save.InvocSlot1 = "Bababoy";
        if (string.IsNullOrEmpty(save.InvocSlot2))
            save.InvocSlot2 = "Name";
        if (string.IsNullOrEmpty(save.InvocSlot3))
            save.InvocSlot3 = "Junktrap";
        if (string.IsNullOrEmpty(save.InvocSlot4))
            save.InvocSlot4 = "Hermes";

        ShowRandomInvocations();
    }

    private void ShowRandomInvocations()
    {
        List<string> allInvocations = new List<string>
        {
            save.InvocSlot1,
            save.InvocSlot2,
            save.InvocSlot3,
            save.InvocSlot4
        };

        HashSet<int> selectedIndices = new HashSet<int>();
        while (selectedIndices.Count < 3)
        {
            int randomIndex = Random.Range(0, allInvocations.Count);
            selectedIndices.Add(randomIndex);
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
