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
    
    public void MainTitle()
    {
        SceneManager.LoadScene(0);
    }
    public void Upgrades()
    {
        PanelUpgrades.SetActive(true);    
        PanelWeapons.SetActive(false);    
        PanelInvoc.SetActive(false);     
        PanelSpecial.SetActive(false);
    }
    public void Weapons()
    {
        PanelUpgrades.SetActive(false);    
        PanelWeapons.SetActive(true);    
        PanelInvoc.SetActive(false);     
        PanelSpecial.SetActive(false);
    }
    public void Invoc()
    {
        PanelUpgrades.SetActive(false);    
        PanelWeapons.SetActive(false);    
        PanelInvoc.SetActive(true);     
        PanelSpecial.SetActive(false);
    }
    public void Special()
    {
        PanelUpgrades.SetActive(false);    
        PanelWeapons.SetActive(false);    
        PanelInvoc.SetActive(true);     
        PanelSpecial.SetActive(false);
    }
}
