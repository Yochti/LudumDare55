using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocationShopAppear : MonoBehaviour
{
    public saveSytem save;

    public GameObject[] invocPanels; // Array de panels pour les invocations

    // Positions associées aux numéros d'invocations
    private Vector3[] targetPositions = new Vector3[]
    {
        new Vector3(-900, -125, 0),  // Position pour 1
        new Vector3(-300, -125, 0),  // Position pour 2
        new Vector3(300, -125, 0),   // Position pour 3
        new Vector3(900, -125, 0)    // Position pour 4
    };

    // Dictionnaire pour associer les noms d'invocations à leurs panels
    private Dictionary<string, GameObject> invocMapping;

    private void Start()
    {
        // Associe les noms d'invocations aux panels correspondants
        invocMapping = new Dictionary<string, GameObject>
        {
            { "Bababoy", invocPanels[0] },
            { "Name", invocPanels[1] },
            { "Junktrap", invocPanels[2] },
            { "Hermes", invocPanels[3] },
            { "Dimitri", invocPanels[4] }
        };

        // Charger les données sauvegardées
        save.LoadData();

        // Traite chaque invocation slot
        ApplyInvocationPosition(save.InvocSlot1, 1);
        ApplyInvocationPosition(save.InvocSlot2, 2);
        ApplyInvocationPosition(save.InvocSlot3, 3);
        ApplyInvocationPosition(save.InvocSlot4, 4);
    }

    // Active ou désactive un panel et change sa position selon le numéro
    private void ApplyInvocationPosition(string invocName, int positionNumber)
    {
        if (invocMapping.ContainsKey(invocName))
        {
            GameObject invocPanel = invocMapping[invocName];

            // Active le panel si une invocation est liée à cette position
            invocPanel.SetActive(true);

            // Si les panels sont des éléments UI, on utilise RectTransform pour ajuster la position
            RectTransform rectTransform = invocPanel.GetComponent<RectTransform>();

            if (rectTransform != null && positionNumber >= 1 && positionNumber <= 4)
            {
                rectTransform.anchoredPosition = targetPositions[positionNumber - 1];
            }
            else if (positionNumber >= 1 && positionNumber <= 4)
            {
                invocPanel.transform.position = targetPositions[positionNumber - 1];
            }
        }
    }
}
