using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HealthBar : MonoBehaviour
{
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI SoulsText;
    public PlayerSoulsCollect playerSouls;
    public PlayerHealth HealthInt;

    void Update()
    {
        HealthText.text = HealthInt.currentHealth.ToString();
        SoulsText.text = playerSouls.soulValue.ToString();

    }
}
