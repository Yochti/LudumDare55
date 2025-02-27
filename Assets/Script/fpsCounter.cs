using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class fpsCounter : MonoBehaviour
{
    public Slider FpsSlider;
    public TextMeshProUGUI textFPS;
    public saveSytem save;
    private void Start()
    {
        if (FpsSlider != null)
            FpsSlider.value = save.MaxFps;
        else
            FpsSlider.value = 240;
    }
    private void Update()
    {
        textFPS.text = (FpsSlider.value * 10).ToString() + " FPS";
        int targetFPS = Mathf.RoundToInt(FpsSlider.value * 10);
        Application.targetFrameRate = targetFPS;
        save.MaxFps = targetFPS;
        save.SaveData();
    }
}
