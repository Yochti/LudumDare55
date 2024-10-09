using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGestionOptionPanel : MonoBehaviour
{
    public GameObject border1;
    public GameObject border2;
    public GameObject border3;
    public GameObject border4;

    //Panel
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;

    public void button1()
    {
        border1.SetActive(true);
        border2.SetActive(false);
        border3.SetActive(false);
        border4.SetActive(false);
        panel1.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(false);

    }
    public void button2()
    {
        border1.SetActive(false);
        border2.SetActive(true);
        border3.SetActive(false);
        border4.SetActive(false);
        panel1.SetActive(false);
        panel2.SetActive(true);
        panel3.SetActive(false);
        panel4.SetActive(false);

    }
    public void button3()
    {
        border1.SetActive(false);
        border2.SetActive(false);
        border3.SetActive(true);
        border4.SetActive(false);
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(true);
        panel4.SetActive(false);

    }
    public void button4()
    {
        border1.SetActive(false);
        border2.SetActive(false);
        border3.SetActive(false);
        border4.SetActive(true);
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(true);

    }
}
