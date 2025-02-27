using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class soulcounter2 : MonoBehaviour
{
    public TextMeshProUGUI soulsCount;
    public saveSytem save;
    private void Update()
    {
        soulsCount.text = save.TotalSouls.ToString();
    }
}
