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

    // R�f�rences aux composants TextMeshProUGUI pour le nom et la description
    public TextMeshProUGUI NameTMP; // R�f�rence pour le nom
    public TextMeshProUGUI DescriptionTMP; // R�f�rence pour la description

    // Variables pour stocker les descriptions traduites
    private string translatedName;
    private string translatedDescription;

    private void Start()
    {
        // Charger les traductions des noms et descriptions au d�marrage
        localizedName.StringChanged += (translatedText) => { translatedName = translatedText; };
        localizedDescription.StringChanged += (translatedText) => { translatedDescription = translatedText; };

        // Rafra�chir les traductions
        localizedName.RefreshString();
        localizedDescription.RefreshString();
    }

    private void Update()
    {
        // Mettre � jour le total des �mes en permanence
        totalSoulsTMP.text = save.TotalSouls.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imageRight.sprite = healthSprite;

        // Met � jour les �l�ments visuels avec les textes traduits
        UpdateTexts();

        // Prix inchang�
        PriceTMP.text = price;

        SetSliderValueD();
        SetSliderValueS();
    }

    private void UpdateTexts()
    {
        // Met � jour les textes de nom et de description avec les traductions
        NameTMP.text = translatedName; // Met � jour le nom avec la traduction
        DescriptionTMP.text = translatedDescription; // Met � jour la description avec la traduction
    }

    public void SetSliderValueD()
    {
        // D�finir la valeur du slider directement
        sliderDamage.value = Mathf.Clamp(sliderDamagef, sliderDamage.minValue, sliderDamage.maxValue);
    }

    public void SetSliderValueS()
    {
        // D�finir la valeur du slider directement
        sliderSpeed.value = Mathf.Clamp(sliderSpeedf, sliderSpeed.minValue, sliderSpeed.maxValue);
    }

    private void OnDestroy()
    {
        // D�sabonner les �v�nements pour �viter les fuites de m�moire
        localizedName.StringChanged -= (translatedText) => { translatedName = translatedText; };
        localizedDescription.StringChanged -= (translatedText) => { translatedDescription = translatedText; };
    }
}
