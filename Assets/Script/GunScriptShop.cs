using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Video;

public class GunScriptShop : MonoBehaviour, IPointerEnterHandler
{
    public VideoPlayer videoPlayer;
    public VideoClip videoClip;
    public OnHoverScaleUp hoverS;
    public LocalizedString localizedName;

    public TextMeshProUGUI PriceTMP;
    public saveSytem save;
    public string price;

    [field:SerializeField] public static bool hasClicked = false; // Variable statique partagée par tous les objets GunScriptShop

    public TextMeshProUGUI NameTMP; // Référence pour le nom

    private string translatedName;

    private void Start()
    {
        // Charger les traductions des noms et descriptions au démarrage
        localizedName.StringChanged += (translatedText) => { translatedName = translatedText; };
        localizedName.RefreshString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hasClicked) return; // Bloquer si un bouton a été cliqué
        UpdateTexts();
        videoPlayer.clip = videoClip;
        PriceTMP.text = price;
    }

    private void UpdateTexts()
    {
        NameTMP.text = translatedName; // Met à jour le nom avec la traduction
    }
    public void ToggleClick(GameObject gameObject)
    {
        hasClicked = !hasClicked;
        hoverS.hoverExit();
        if(!hasClicked)gameObject.transform.localScale *= 1.25f;
    }
    /*public void OnPointerClick(PointerEventData eventData)
    {
        if (hasClicked)
        {
            hasClicked = false; // Réactive OnPointerEnter pour tous les boutons
        }
        else
        {
            hasClicked = true; // Désactive OnPointerEnter pour tous les boutons
        }
    }*/

    private void OnDestroy()
    {
        // Désabonner les événements pour éviter les fuites de mémoire
        localizedName.StringChanged -= (translatedText) => { translatedName = translatedText; };
    }
}
