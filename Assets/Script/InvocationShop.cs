using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class InvocationShop : MonoBehaviour, IPointerEnterHandler
{
    public Sprite healthSprite;
    public Image imageRight;

    // LocalizedString uniquement pour la description
    public LocalizedString localizedDescription;

    public TextMeshProUGUI HealthDescription;
    public TextMeshProUGUI NameTMP;
    public TextMeshProUGUI PriceTMP;
    public saveSytem save;
    public string Name;
    public string price;
    public TextMeshProUGUI totalSoulsTMP;

    // Variable pour stocker la description traduite
    private string translatedDescription;

    private void Start()
    {
        // Charger la traduction de la description au démarrage
        localizedDescription.StringChanged += (translatedText) => { translatedDescription = translatedText; };

        // Rafraîchir la description traduite
        localizedDescription.RefreshString();
    }

    private void Update()
    {
        // Mettre à jour le total des âmes en permanence
        totalSoulsTMP.text = save.TotalSouls.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Met à jour les éléments visuels avec la description traduite
        imageRight.sprite = healthSprite;
        HealthDescription.text = translatedDescription;
        NameTMP.text = Name; // Nom inchangé
        PriceTMP.text = price; // Prix inchangé
    }

    private void OnDestroy()
    {
        // Désabonner l'événement pour éviter les fuites de mémoire
        localizedDescription.StringChanged -= (translatedText) => { translatedDescription = translatedText; };
    }
}
