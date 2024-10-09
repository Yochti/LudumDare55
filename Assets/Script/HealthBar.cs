using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HealthBar : MonoBehaviour
{
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI SoulsText;
    public TextMeshProUGUI recentSoulsText;
    public PlayerHealth HealthInt;

    private void Start()
    {
        SoulsText.text = PlayerSoulsCollect.soulValue.ToString();
    }
    void Update()
    {
        if(PlayerSoulsCollect.recentSoulsValue >= 1)
        {
            recentSoulsText.text = "+" + PlayerSoulsCollect.recentSoulsValue.ToString();
            recentSoulsText.gameObject.SetActive(true);
            return;
        }
        recentSoulsText.gameObject.SetActive(false);
        HealthText.text = HealthInt.currentHealth.ToString();

    }
}
