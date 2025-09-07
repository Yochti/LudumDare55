using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class statsText : MonoBehaviour
{
    public TextMeshProUGUI textHp;
    public TextMeshProUGUI textMs;
    public TextMeshProUGUI textDmg;
    public TextMeshProUGUI textAttackSpeed;
    public TextMeshProUGUI textCritRate;
    public TextMeshProUGUI textCritDmg;
    public TextMeshProUGUI textLuck;


    private void OnEnable()
    {
        textHp.text = PlayerHealth.maxHealth.ToString();
        textMs.text = PlayerStats.moveSpeed.ToString();
        textDmg.text = PlayerStats.additionalDamage.ToString();
        textAttackSpeed.text = PlayerStats.attackSpeed.ToString();
        textCritRate.text = (PlayerStats.critRate).ToString();
        textCritDmg.text = (PlayerStats.critDamage).ToString();
        textLuck.text = PlayerStats.luck.ToString();

    }
}
