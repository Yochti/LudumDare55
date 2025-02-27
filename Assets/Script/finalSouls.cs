using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FinalSoul: MonoBehaviour
{
    public TextMeshProUGUI soulsTMP;
    public TextMeshProUGUI soulsTMP1;
    void Update()
    {
        soulsTMP.text ="+" + PlayerHealth.finalSouls.ToString();
        soulsTMP1.text ="+" + PlayerHealth.finalSouls.ToString();
    }

   
}
