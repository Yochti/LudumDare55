using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class saveSytem : MonoBehaviour
{
    public int TotalSouls;
    //Level Upgrades
    public int HealthUpgrades;
    public int DamagesUpgrades;
    public int AttackSpeedUpgrades;
    public int SpeedUpgrades;
    public int HealthRegen;
    public int InvincibilityFrame;
    public int CritChance;
    public int CritDamage;
    public int SoulsAttract;

    //GameMode
    public string whichGameMode;

    //---- SETTINGS ----
    //Audio Volume 
    public float GlobalVolume;
    public float MusicVolume;
    public float SFXVolume;
    // FPS RESTRICT 
    public int MaxFps;
    //Key Bindings
    public string UpKey;
    public string DownKey;
    public string LeftKey;
    public string RightKey;
    public string DashKey;

    //References Caracteristique
    public int PlayerHealth;
    public int PlayerDamages;
    public float PlayerAttackSpeed;
    public float PlayerMoveSpeed;
    public int PlayerRegen;
    public float PlayerFrame;
    public int PlayerCritchance;
    public float PlayerCritDamages;
    public int PlayerSoulsA;

    //Invoc
    public string InvocSlot1;
    public string InvocSlot2;
    public string InvocSlot3;
    public string InvocSlot4;

    //Weapons
    public bool hasGun1;  
    public bool hasGun2;  
    public bool hasGun3;  
    public bool hasGun4;  
    public bool hasGun5;  
    public bool hasGun6;  
    public bool hasGun7;
    public bool hasGun8;
    public string whichWeapon;

    private void Awake()
    {
        LoadData();
    }
    private void Update()
    {

        
    }

    public void SaveData()
    {
        SaveData savedData = new SaveData
        {
            WhichGameMode = whichGameMode,
            totalSouls = TotalSouls,
            healthUpgrades = HealthUpgrades,
            damagesUpgrades = DamagesUpgrades,
            attackSpeedUpgrades = AttackSpeedUpgrades,
            speedUpgrades = SpeedUpgrades,
            healthRegen = HealthRegen,
            invincibilityFrame = InvincibilityFrame,
            critChance = CritChance,
            critDamage = CritDamage,
            soulsAttract = SoulsAttract,
            //carac
            playerHealth = PlayerHealth,
            playerDamages = PlayerDamages,
            playerAttackSpeed = PlayerAttackSpeed,
            playerMoveSpeed = PlayerMoveSpeed,
            playerRegen = PlayerRegen,
            playerFrame = PlayerFrame,
            playerCritchance = PlayerCritchance,
            playerCritDamages = PlayerCritDamages,
            playerSoulsA = PlayerSoulsA,
            //Invoc
            invocSlot1 = InvocSlot1,
            invocSlot2 = InvocSlot2,
            invocSlot3 = InvocSlot3,
            invocSlot4 = InvocSlot4,
            //Weapons
            HasGun1 = hasGun1,
            HasGun2 = hasGun2,
            HasGun3 = hasGun3,
            HasGun4 = hasGun4,
            HasGun5 = hasGun5,
            HasGun6 = hasGun6,
            HasGun7 = hasGun7,
            HasGun8 = hasGun8,
            WhichWeapon = whichWeapon,
            maxFps = MaxFps,
            //Audio Source
            globalVolume = GlobalVolume,
            musicVolume = MusicVolume,
            sfxVolume = SFXVolume,
            //Key Bidings
            upKey = UpKey,
            downKey = DownKey,
            leftKey = LeftKey,
            rightKey = RightKey,
            dashKey = DashKey,


        };
        string jsonData = JsonUtility.ToJson(savedData);
        string filePath = Application.persistentDataPath + "/SavoData.json";

        // Forcer l'écriture dans le fichier
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(jsonData);
            }
        }

        //Debug.Log("Sauvegarde effectuée. Contenu du fichier JSON: " + jsonData);
    }

    public void LoadData()
    {
        string filePath = Application.persistentDataPath + "/SavoData.json";
        string jsonData = System.IO.File.ReadAllText(filePath);

        SaveData savedData = JsonUtility.FromJson<SaveData>(jsonData);
        TotalSouls = savedData.totalSouls;
        HealthUpgrades = savedData.healthUpgrades;
        DamagesUpgrades = savedData.damagesUpgrades;
        AttackSpeedUpgrades = savedData.attackSpeedUpgrades;
        SpeedUpgrades = savedData.speedUpgrades;
        HealthRegen = savedData.healthUpgrades;
        InvincibilityFrame = savedData.invincibilityFrame;
        CritChance = savedData.critChance;
        CritDamage = savedData.critDamage;
        SoulsAttract = savedData.soulsAttract;

        whichGameMode = savedData.WhichGameMode;

        //Carac
        PlayerHealth = savedData.playerHealth;
        PlayerDamages = savedData.playerDamages;
        PlayerAttackSpeed = savedData.playerAttackSpeed;
        PlayerMoveSpeed = savedData.playerMoveSpeed;
        PlayerRegen = savedData.playerRegen;
        PlayerFrame = savedData.playerFrame;
        PlayerCritchance = savedData.playerCritchance;
        PlayerCritDamages = savedData.playerCritDamages;
        PlayerSoulsA = savedData.playerSoulsA;
        //Weapons
        hasGun1 = savedData.HasGun1;
        hasGun2 = savedData.HasGun2;
        hasGun3 = savedData.HasGun3;
        hasGun4 = savedData.HasGun4;
        hasGun5 = savedData.HasGun5;
        hasGun6 = savedData.HasGun6;
        hasGun7 = savedData.HasGun7;
        hasGun8 = savedData.HasGun8;
        whichWeapon = savedData.WhichWeapon;
        //Invoc
        InvocSlot1 = savedData.invocSlot1;
        InvocSlot2 = savedData.invocSlot2;
        InvocSlot3 = savedData.invocSlot3;
        InvocSlot4 = savedData.invocSlot4;

        //FPS
        MaxFps = savedData.maxFps;
        //Audio Volume
        GlobalVolume = savedData.globalVolume;
        MusicVolume = savedData.musicVolume;
        SFXVolume = savedData.sfxVolume;
        //Key Bidings
        UpKey = savedData.upKey;
        DownKey = savedData.downKey;
        LeftKey = savedData.leftKey;
        RightKey = savedData.rightKey;
        DashKey = savedData.dashKey;


    }
}
public class SaveData
{
    public int totalSouls;
    public int healthUpgrades;
    public int damagesUpgrades;
    public int attackSpeedUpgrades;
    public int speedUpgrades;
    public int healthRegen;
    public int invincibilityFrame;
    public int critChance;
    public int critDamage;
    public int soulsAttract;

    public string WhichGameMode;
    //---- SETTINGS ----
    //FPS
    public int maxFps;
    // Audio Volume
    public float globalVolume;
    public float musicVolume;
    public float sfxVolume;
    //Key Bidings
    public string upKey;
    public string downKey;
    public string leftKey;
    public string rightKey;
    public string dashKey;
    //Carac
    public int playerHealth;
    public int playerDamages;
    public float playerAttackSpeed;
    public float playerMoveSpeed;
    public int playerRegen;
    public float playerFrame;
    public int playerCritchance;
    public float playerCritDamages;
    public int playerSoulsA;

    //Invoc
    public string invocSlot1;
    public string invocSlot2;
    public string invocSlot3;
    public string invocSlot4;
    //Weapons
    public bool HasGun1;
    public bool HasGun2;
    public bool HasGun3;
    public bool HasGun4;
    public bool HasGun5;
    public bool HasGun6;
    public bool HasGun7;
    public bool HasGun8;

    public string WhichWeapon;
}
