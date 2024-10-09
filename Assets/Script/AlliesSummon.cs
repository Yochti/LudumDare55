using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class AlliesSummon : MonoBehaviour
{
    // script
    public PlayerHealth playerHealth;
    public Ally1Controller ally1;
    public AllyHealer ally2;
    public Ally3Controller ally3;
    public Ally4Controller ally4;
    public BulletLife bulletAlly;
    public HealBullet bulletH;
    public portal1 p1;
    public PortalDivision p2;
    // GameObject
    public GameObject player;
    public GameObject friendHolder;

    public GameObject bababoy;
    public GameObject bababoy2;
    public GameObject namee;
    public GameObject JunkTrap;
    public GameObject heiMERDEinger;
    public GameObject Dimitri;

    private int price = 100;
    public bool lvlToRevive;
    public Image BigHealthBar;
    public Slider slider;

    // Status ally
    public string bababoyStatus; 
    public string nameStatus; 
    public string junktrapStatus; 
    public string hStatus; 
    public string dimitriStatus;

    //Text
    public TextMeshProUGUI bababoyTxt1;
    public TextMeshProUGUI bababoyTxt2;
    public TextMeshProUGUI bababoyTxt3;    
    public TextMeshProUGUI NameTxt1;
    public TextMeshProUGUI NameTxt2;
    public TextMeshProUGUI NameTxt3;    
    public TextMeshProUGUI JunkTxt1;
    public TextMeshProUGUI JunkTxt2;
    public TextMeshProUGUI JunkTxt3;    
    public TextMeshProUGUI HTxt1;
    public TextMeshProUGUI HTxt2;
    public TextMeshProUGUI HTxt3;
    public TextMeshProUGUI DTxt1;
    public TextMeshProUGUI DTxt2;
    public TextMeshProUGUI DTxt3;

    public TextMeshProUGUI BababoyLvl;
    public TextMeshProUGUI NameLvl;
    public TextMeshProUGUI JunkLvl;
    public TextMeshProUGUI HLvl;
    public TextMeshProUGUI DLvl;
    
    public TextMeshProUGUI BababoyPrice;
    public TextMeshProUGUI NamePrice;
    public TextMeshProUGUI JunkPrice;
    public TextMeshProUGUI HPrice;
    public TextMeshProUGUI DPrice;

    private void Start()
    {
        bababoyStatus = "0";
        nameStatus = "0";
        junktrapStatus = "0";
        hStatus = "0";
        dimitriStatus = "0";
    }

    public void Bababoy()
    {
        if(PlayerSoulsCollect.soulValue >= 100 && bababoyStatus == "0")
        {
            bababoyTxt1.gameObject.SetActive(false);
            bababoyTxt2.gameObject.SetActive(true);
            BababoyLvl.text = 1.ToString();
            BababoyPrice.text = 200.ToString();
            bababoy.SetActive(true);
            PlayerSoulsCollect.soulValue -= price;
            friendHolder.transform.position = player.transform.position;
            bababoyStatus = "1";
        }
        if(PlayerSoulsCollect.soulValue >= 200 && bababoyStatus =="1")
        {
            bababoyTxt2.gameObject.SetActive(false);
            BababoyLvl.text = 2.ToString();
            BababoyPrice.text = 500.ToString();

            bababoyTxt3.gameObject.SetActive(true);
            PlayerSoulsCollect.soulValue -= 200;
            ally1.shootingCooldown = 0.2f;
            ally1.bulletSpeed = 30f;
            bulletAlly.bulletDamage += 5;
            bababoyStatus = "2";
        }
        if(PlayerSoulsCollect.soulValue >= 500 && bababoyStatus == "2") {
            PlayerSoulsCollect.soulValue -= 500;
            BababoyLvl.text = 3.ToString();
            bababoyTxt2.gameObject.SetActive(false);
            bababoyTxt3.gameObject.SetActive(true);
            bababoy2.SetActive(true);
            bababoy.SetActive(false);
        }

    }
    public void Name()
    {
        if (PlayerSoulsCollect.soulValue >= 100 && nameStatus == "0")
        {
            NameLvl.text = 1.ToString();
            NameTxt1.gameObject.SetActive(false);
            NameTxt2.gameObject.SetActive(true);
            namee.SetActive(true);
            PlayerSoulsCollect.soulValue -= price;
            playerHealth.maxHealth += 100;
            playerHealth.currentHealth += 100;
            nameStatus = "1";
            NamePrice.text = 200.ToString();
            slider.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 72);
            friendHolder.transform.position = player.transform.position;

            BigHealthBar.rectTransform.sizeDelta = new Vector2(528, 100);
        }
        if (nameStatus == "1" && PlayerSoulsCollect.soulValue >= 200)
        {
            NameLvl.text = 2.ToString();
            NamePrice.text = 500.ToString();

            NameTxt2.gameObject.SetActive(false);
            NameTxt3.gameObject.SetActive(true);
            nameStatus = "2";
            PlayerSoulsCollect.soulValue -= 200;
            playerHealth.maxHealth += 200;
            playerHealth.currentHealth += 200;
            ally2.healingInterval = 15;
            slider.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 72);
            BigHealthBar.rectTransform.sizeDelta = new Vector2(760, 100);


        }
        if (nameStatus == "2" && PlayerSoulsCollect.soulValue >= 500)
        {
            NameLvl.text = 3.ToString();

            PlayerSoulsCollect.soulValue -= 500;
            lvlToRevive = true;
            ally2.canRevive = true;
        }

    }
   
    public void jUNKTRAP()
    {
        if (PlayerSoulsCollect.soulValue >= 100 && junktrapStatus == "0")
        {
            JunkPrice.text = 200.ToString();
            JunkLvl.text = 1.ToString();
            JunkTxt1.gameObject.SetActive(false);
            JunkTxt2.gameObject.SetActive(true);
            JunkTrap.SetActive(true);
            PlayerSoulsCollect.soulValue -= price;
            junktrapStatus = "1";
            friendHolder.transform.position = player.transform.position;

        }
        if (junktrapStatus == "1" && PlayerSoulsCollect.soulValue >= 200)
        {
            JunkLvl.text = 2.ToString();
            JunkPrice.text = 500.ToString();

            JunkTxt2.gameObject.SetActive(false);
            JunkTxt3.gameObject.SetActive(true);
            ally3.trapPlacementInterval -= 6;
            junktrapStatus = "2";
            PlayerSoulsCollect.soulValue -= 200;

        }
        if (junktrapStatus == "2" && PlayerSoulsCollect.soulValue >= 500)
        {
            JunkLvl.text = 3.ToString();

            junktrapStatus = "3";
            PlayerSoulsCollect.soulValue -= 500;

        }

    }

    public void HeiMERDEinger()
    {
        if (PlayerSoulsCollect.soulValue >= 100 && hStatus == "0")
        {
            HPrice.text = 200.ToString();
            HLvl.text = 1.ToString();
            HTxt1.gameObject.SetActive(false);
            HTxt2.gameObject.SetActive(true);
            heiMERDEinger.SetActive(true);
            PlayerSoulsCollect.soulValue -= price;
            friendHolder.transform.position = player.transform.position;

            hStatus = "1";
        }
        if (hStatus == "1" && PlayerSoulsCollect.soulValue >= 200)
        {
            HLvl.text = 2.ToString();
            HPrice.text = 500.ToString();

            HTxt2.gameObject.SetActive(false);
            HTxt3.gameObject.SetActive(true);
            ally4.maxTurrets = 5;
            bulletH.healAmount += 1;
            bulletAlly.bulletDamage += 5;
            hStatus = "2";
            PlayerSoulsCollect.soulValue -= 200;

        }
        if (hStatus == "2" && PlayerSoulsCollect.soulValue >= 500)
        {
            HLvl.text = 3.ToString();

            PlayerSoulsCollect.soulValue -= 500;
            hStatus = "3";

        }

    }

    public void DimitriVoid()
    {
        if (PlayerSoulsCollect.soulValue >= 100 && dimitriStatus == "0")
        {
            DPrice.text = 200.ToString();
            DLvl.text = 1.ToString();
            Dimitri.SetActive(true);
            DTxt1.gameObject.SetActive(false);
            DTxt2.gameObject.SetActive(true);
            PlayerSoulsCollect.soulValue -= price;
            dimitriStatus = "1";
            friendHolder.transform.position = player.transform.position;

        }
        if (dimitriStatus == "1" && PlayerSoulsCollect.soulValue >= 200)
        {
            DPrice.text = 500.ToString();
            DLvl.text = 2.ToString();
            DTxt2.gameObject.SetActive(false);
            DTxt3.gameObject.SetActive(true);
            p2.numberOfBullet += 2;
            dimitriStatus = "2";
            PlayerSoulsCollect.soulValue -= 200;

        }
        if (dimitriStatus == "2" && PlayerSoulsCollect.soulValue >= 500)
        {
            DLvl.text = 3.ToString();

            dimitriStatus = "3";
            PlayerSoulsCollect.soulValue -= 500;

        }

    }

}
