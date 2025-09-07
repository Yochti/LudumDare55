using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipSuper : MonoBehaviour
{
    public saveSytem save;
    public Image backgroundColor;
    public Color equippedColor;
    public Color baseColor;
    public string nameSuper;

    private void Update()
    {
        if (nameSuper == save.whichSpecial)
        {
            backgroundColor.color = equippedColor;
        }
        else
        {
            backgroundColor.color = baseColor;
        }
    }

    public void EquipSuper()
    {
        if (IsSuperOwned(nameSuper))
        {
            save.whichSpecial = nameSuper;
            save.SaveData();
        }
        else
        {
            Debug.Log("Ce super n'est pas encore débloqué.");
        }
    }

    private bool IsSuperOwned(string superName)
    {
        return superName == "Special1" ||
               (superName == "Special2" && save.hasSpecial2) ||
               (superName == "Special3" && save.hasSpecial3);

    }
}
