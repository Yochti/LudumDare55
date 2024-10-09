using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class onHoverButton : MonoBehaviour, IPointerEnterHandler
{
    public GameObject textSelection1;
    public GameObject textSelection2;
    public GameObject textSelection3;
    public GameObject textSelection4;
    public AudioSource aS;

    public void OnPointerEnter(PointerEventData eventData)
    {
        textSelection1.SetActive(true);
        textSelection2.SetActive(false);
        textSelection3.SetActive(false);    
        textSelection4.SetActive(false);
        aS.Play();
    }
    
}
