using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class xpManager : MonoBehaviour
{
    public TextMeshProUGUI textXP;
    public TextMeshProUGUI textLvl;
    public saveSytem save;
    public Slider slider;

    private void OnEnable()
    {
        if (save.lvl == 0) save.lvl = 1;

        // Lance l'animation vers la nouvelle valeur d'XP
        StartCoroutine(AnimateXPBar());
    }

    private IEnumerator AnimateXPBar()
    {
        float xpToAdd = save.xp;
        int lvl = save.lvl;
        float maxXp = lvl * 100f;
        float currentXp = 0f;
        slider.value = 0f;

        while (xpToAdd >= maxXp)
        {
            yield return StartCoroutine(FillXPBar(currentXp, maxXp, lvl));
            xpToAdd -= maxXp;
            lvl++;
            maxXp = lvl * 100f;
            currentXp = 0f;
        }

        yield return StartCoroutine(FillXPBar(currentXp, xpToAdd, lvl));

        // Mise à jour finale des valeurs
        save.xp = xpToAdd;
        save.lvl = lvl;
        save.SaveData();
    }

    private IEnumerator FillXPBar(float fromXp, float toXp, int lvl)
    {
        float duration = 0.8f;
        float elapsed = 0f;
        float maxXp = lvl * 100f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float currentXp = Mathf.Lerp(fromXp, toXp, elapsed / duration);
            slider.value = currentXp / maxXp;
            textXP.text = Mathf.FloorToInt(currentXp) + "/" + maxXp;
            textLvl.text = "LV" + lvl;
            yield return null;
        }

        slider.value = toXp / maxXp;
        textXP.text = Mathf.FloorToInt(toXp) + "/" + maxXp;
        textLvl.text = "LV" + lvl;
    }
}
