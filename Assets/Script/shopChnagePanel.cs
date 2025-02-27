using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ShopChnagePanel : MonoBehaviour
{
    public GameObject PanelUpgrades;
    public GameObject PanelWeapons;
    public GameObject PanelInvoc;
    public GameObject PanelSpecial;
    public AudioSource audioo;
    public void MainTitle()
    {
        SceneManager.LoadScene(0);
    }
    public void Upgrades()
    {
        audioo.Play();
        PanelUpgrades.SetActive(true);    
        PanelWeapons.SetActive(false);    
        PanelInvoc.SetActive(false);     
        PanelSpecial.SetActive(false);
    }
    public void Weapons()
    {
        audioo.Play();

        PanelUpgrades.SetActive(false);    
        PanelWeapons.SetActive(true);    
        PanelInvoc.SetActive(false);     
        PanelSpecial.SetActive(false);
    }
    public void Invoc()
    {
        audioo.Play();

        PanelUpgrades.SetActive(false);    
        PanelWeapons.SetActive(false);    
        PanelInvoc.SetActive(true);     
        PanelSpecial.SetActive(false);
    }
    public void Special()
    {
        audioo.Play();

        PanelUpgrades.SetActive(false);    
        PanelWeapons.SetActive(false);    
        PanelInvoc.SetActive(false);     
        PanelSpecial.SetActive(true);
    }
}
