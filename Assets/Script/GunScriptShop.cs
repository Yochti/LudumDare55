using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class GunScriptShop : MonoBehaviour, IPointerEnterHandler
{
    public Sprite healthSprite;
    public Image imageRight;

    // Utilisation de LocalizedString pour le nom et la description
    public LocalizedString localizedName;
    public LocalizedString localizedDescription;

    public TextMeshProUGUI PriceTMP; // Ne change pas le prix
    public saveSytem save;
    public string price;
    public float sliderDamagef;
    public float sliderSpeedf;
    public Slider sliderDamage;
    public Slider sliderSpeed;
    public TextMeshProUGUI totalSoulsTMP;

    // Références aux composants TextMeshProUGUI pour le nom et la description
    public TextMeshProUGUI NameTMP; // Référence pour le nom
    public TextMeshProUGUI DescriptionTMP; // Référence pour la description

    // Variables pour stocker les descriptions traduites
    private string translatedName;
    private string translatedDescription;

    private void Start()
    {
        // Charger les traductions des noms et descriptions au démarrage
        localizedName.StringChanged += (translatedText) => { translatedName = translatedText; };
        localizedDescription.StringChanged += (translatedText) => { translatedDescription = translatedText; };

        // Rafraîchir les traductions
        localizedName.RefreshString();
        localizedDescription.RefreshString();
    }

    private void Update()
    {
        // Mettre à jour le total des âmes en permanence
        totalSoulsTMP.text = save.TotalSouls.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imageRight.sprite = healthSprite;

        // Met à jour les éléments visuels avec les textes traduits
        UpdateTexts();

        // Prix inchangé
        PriceTMP.text = price;

        SetSliderValueD();
        SetSliderValueS();
    }

    private void UpdateTexts()
    {
        // Met à jour les textes de nom et de description avec les traductions
        NameTMP.text = translatedName; // Met à jour le nom avec la traduction
        DescriptionTMP.text = translatedDescription; // Met à jour la description avec la traduction
    }

    public void SetSliderValueD()
    {
        // Définir la valeur du slider directement
        sliderDamage.value = Mathf.Clamp(sliderDamagef, sliderDamage.minValue, sliderDamage.maxValue);
    }

    public void SetSliderValueS()
    {
        // Définir la valeur du slider directement
        sliderSpeed.value = Mathf.Clamp(sliderSpeedf, sliderSpeed.minValue, sliderSpeed.maxValue);
    }

    private void OnDestroy()
    {
        // Désabonner les événements pour éviter les fuites de mémoire
        localizedName.StringChanged -= (translatedText) => { translatedName = translatedText; };
        localizedDescription.StringChanged -= (translatedText) => { translatedDescription = translatedText; };
    }
}
