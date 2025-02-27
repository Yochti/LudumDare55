using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
public class LocaleSelector : MonoBehaviour
{
    private bool isActive;
    private void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(ID);
    }
    public void ChangeLocale(int LocaleID)
    {
        if (isActive)
            return;
        StartCoroutine(SetLocale(LocaleID));
    }

    IEnumerator SetLocale(int _localeID)
    {
        isActive = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        PlayerPrefs.SetInt("LocaleKey",_localeID);
        isActive = false;
    }
}
