using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ValueSliderToTMP : MonoBehaviour
{
    //TextMeshPro
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;

    //Slider
    public Slider slider1;
    public Slider slider2;
    public Slider slider3;

    public saveSytem save;
    private void Start()
    {
        slider1.value = save.GlobalVolume;
        slider2.value = save.MusicVolume;
        slider3.value = save.SFXVolume;
    }
    void Update()
    {
        text1.text = (slider1.value * 10).ToString();
        text2.text = (slider2.value * 10).ToString();
        text3.text = (slider3.value * 10).ToString();
    }
}
