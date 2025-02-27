using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class onHoverGameMode : MonoBehaviour, IPointerEnterHandler
{
    // Utilise LocalizedString pour la traduction des titres et descriptions
    public LocalizedString gameModeTitleLocalized;
    public LocalizedString descriptionLocalized;

    public TextMeshProUGUI gameModeTMP;
    public TextMeshProUGUI descriptionTMP;
    public Image gamemodeImage;
    public Sprite gameModeSprite;

    private string translatedTitle;
    private string translatedDescription;

    private void Start()
    {
        // Charger les traductions au démarrage pour chaque instance
        gameModeTitleLocalized.StringChanged += (translatedText) => { translatedTitle = translatedText; };
        descriptionLocalized.StringChanged += (translatedText) => { translatedDescription = translatedText; };

        // Initialiser les valeurs localisées au cas où
        gameModeTitleLocalized.RefreshString();
        descriptionLocalized.RefreshString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Met à jour le texte et l'image pour chaque objet lorsqu'il est survolé
        gameModeTMP.text = translatedTitle;
        descriptionTMP.text = translatedDescription;
        gamemodeImage.sprite = gameModeSprite;
    }

    private void OnDestroy()
    {
        // Désabonner les événements pour éviter les fuites de mémoire
        gameModeTitleLocalized.StringChanged -= (translatedText) => { translatedTitle = translatedText; };
        descriptionLocalized.StringChanged -= (translatedText) => { translatedDescription = translatedText; };
    }
}
