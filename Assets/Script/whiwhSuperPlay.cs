using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whiwhSuperPlayer : MonoBehaviour
{
    public saveSytem save;

    public GameObject Special1;
    public GameObject Special2;
    public GameObject Special3;

    void Start()
    {
        if (string.IsNullOrEmpty(save.whichSpecial) || save.whichSpecial == "Special1")
        {
            Special1.SetActive(true);
            Special2.SetActive(false);
            Special3.SetActive(false);
        }
        else if (save.whichSpecial == "Special2")
        {
            Special1.SetActive(false);
            Special2.SetActive(true);
            Special3.SetActive(false);
        }
        else if (save.whichSpecial == "Special3")
        {
            Special1.SetActive(false);
            Special2.SetActive(false);
            Special3.SetActive(true);
        }
    }
}
