using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GunScriptShop : MonoBehaviour, IPointerEnterHandler
{
    public Sprite healthSprite;
    public Image imageRight;
    public string Name;
    public string Description;
    public TextMeshProUGUI HealthDescription;
    public TextMeshProUGUI NameTMP;
    public TextMeshProUGUI PriceTMP;
    public saveSytem save;
    public string price;
    public float sliderDamagef;
    public float sliderSpeedf;
    public Slider sliderDamage;
    public Slider sliderSpeed;
    public TextMeshProUGUI totalSoulsTMP;

    private void Update()
    {
        totalSoulsTMP.text = save.TotalSouls.ToString();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        imageRight.sprite = healthSprite;
        HealthDescription.text = Description;
        NameTMP.text = Name;
        PriceTMP.text = price;
        SetSliderValueD();
        SetSliderValueS();
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
}
