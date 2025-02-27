using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SoulCounterText : MonoBehaviour
{
    public TextMeshProUGUI soulsCount;
    private void Update()
    {
        soulsCount.text = PlayerSoulsCollect.soulValue.ToString();
    }
}
