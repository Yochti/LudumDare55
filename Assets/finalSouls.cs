using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FinalSoul: MonoBehaviour
{
    public TextMeshProUGUI soulsTMP;
    void Update()
    {
        soulsTMP.text ="+" + PlayerSoulsCollect.soulValue.ToString();
    }

   
}
