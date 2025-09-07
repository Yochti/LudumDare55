using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class soulsCounter : MonoBehaviour
{
    public saveSytem save;
    public TextMeshProUGUI textSouls;

    private void Update()
    {
        textSouls.text = save.totalSouls.ToString();
    }
}
