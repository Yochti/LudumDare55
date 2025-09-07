using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class saveSytem : MonoBehaviour
{
    public int totalSouls;
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
    public int Luck;
    //GameMode
    public string whichGameMode;

    //---- SETTINGS ----
    //Audio Volume 
    public float GlobalVolume;
    public float MusicVolume;
    public float SFXVolume;
    public bool isAutoShoot;
    public bool isAutoAim;
    // FPS RESTRICT 
    public int MaxFps;


    //References Caracteristique
    public int PlayerHealth;
    public float PlayerDamages;
    public float PlayerAttackSpeed;
    public float PlayerMoveSpeed;
    public int PlayerRegen;
    public float PlayerFrame;
    public int PlayerCritchance;
    public float PlayerCritDamages;
    public int PlayerSoulsA;
    public int PlayerLuck;
    //Invoc
    public string InvocSlot1;
    public string InvocSlot2;
    public string InvocSlot3;
    public string InvocSlot4;

    public bool firstFight;
    //Invoc
    public bool hasInvoc5;
    public bool hasInvoc6;
    public bool hasInvoc7;
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

    //Special
    public bool hasSpecial1 = true;
    public bool hasSpecial2;
    public bool hasSpecial3;
    public string whichSpecial;
    //XP
    public int lvl;
    public float xp;
    //difficulty
    public bool generalDifficulty;
    public bool enemyAmountDifficulty;
    public bool xpAmountDifficulty;
    public bool rerollDifficulty;
    public bool noDashDifficulty;
    public bool noChestDifficulty;
    public bool noInvocDifficulty;
    public bool noDrop;
    public float percentageBonus;
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
            TotalSouls = totalSouls,
            healthUpgrades = HealthUpgrades,
            damagesUpgrades = DamagesUpgrades,
            attackSpeedUpgrades = AttackSpeedUpgrades,
            speedUpgrades = SpeedUpgrades,
            healthRegen = HealthRegen,
            invincibilityFrame = InvincibilityFrame,
            critChance = CritChance,
            critDamage = CritDamage,
            soulsAttract = SoulsAttract,
            luck = Luck,
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
            playerLuck = PlayerLuck,
            //Invoc
            invocSlot1 = InvocSlot1,
            invocSlot2 = InvocSlot2,
            invocSlot3 = InvocSlot3,
            invocSlot4 = InvocSlot4,
            HasInvoc5 = hasInvoc5,
            HasInvoc6 = hasInvoc6,
            HasInvoc7 = hasInvoc7,
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
            IsAutoAim = isAutoAim,
            IsAutoShoot = isAutoShoot,

            //Special
            HasSpecial1 = hasSpecial1,
            HasSpecial2 = hasSpecial2,
            HasSpecial3 = hasSpecial3,
            WhichSpecial = whichSpecial,
            //Xp
            Lvl = lvl,
            Xp = xp,
            //Difficulty
            GeneralDifficulty = generalDifficulty,
            EnemyAmountDifficulty = enemyAmountDifficulty,
            XpAmountDifficulty = xpAmountDifficulty,
            RerollDifficulty = rerollDifficulty,
            NoDashDifficulty = noDashDifficulty,
            NoChestDifficulty = noChestDifficulty,
            PercentageBonus = percentageBonus,
            NoInvocDifficulty = noInvocDifficulty,
            NoDrop = noDrop,
            //Audio Source
            globalVolume = GlobalVolume,
            musicVolume = MusicVolume,
            sfxVolume = SFXVolume,

            FirstFight = firstFight,

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
        totalSouls = savedData.TotalSouls;
        HealthUpgrades = savedData.healthUpgrades;
        DamagesUpgrades = savedData.damagesUpgrades;
        AttackSpeedUpgrades = savedData.attackSpeedUpgrades;
        SpeedUpgrades = savedData.speedUpgrades;
        HealthRegen = savedData.healthUpgrades;
        InvincibilityFrame = savedData.invincibilityFrame;
        CritChance = savedData.critChance;
        CritDamage = savedData.critDamage;
        SoulsAttract = savedData.soulsAttract;
        Luck = savedData.luck;
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
        PlayerLuck = savedData.playerLuck;
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
        //Special
        hasSpecial1 = savedData.HasSpecial1;
        hasSpecial2 = savedData.HasSpecial2;
        hasSpecial3 = savedData.HasSpecial3;
        whichSpecial = savedData.WhichSpecial;
        //XP
        xp = savedData.Xp;
        lvl = savedData.Lvl;
        //Difficulty
        generalDifficulty = savedData.GeneralDifficulty;
        xpAmountDifficulty = savedData.XpAmountDifficulty;
        enemyAmountDifficulty = savedData.EnemyAmountDifficulty;
        rerollDifficulty = savedData.RerollDifficulty;
        noDashDifficulty = savedData.NoDashDifficulty;
        noChestDifficulty = savedData.NoChestDifficulty;
        noInvocDifficulty = savedData.NoInvocDifficulty;
        noDrop = savedData.NoDrop;
        percentageBonus = savedData.PercentageBonus;
        //Invoc
        InvocSlot1 = savedData.invocSlot1;
        InvocSlot2 = savedData.invocSlot2;
        InvocSlot3 = savedData.invocSlot3;
        InvocSlot4 = savedData.invocSlot4;
        hasInvoc5 = savedData.HasInvoc5;
        hasInvoc6 = savedData.HasInvoc6;
        hasInvoc7 = savedData.HasInvoc7;
        //FPS
        MaxFps = savedData.maxFps;
        //Audio Volume
        GlobalVolume = savedData.globalVolume;
        MusicVolume = savedData.musicVolume;
        SFXVolume = savedData.sfxVolume;
        isAutoAim = savedData.IsAutoAim;
        isAutoShoot = savedData.IsAutoShoot;

        firstFight = savedData.FirstFight;

    }
}
public class SaveData
{
    public int TotalSouls;
    public int healthUpgrades;
    public int damagesUpgrades;
    public int attackSpeedUpgrades;
    public int speedUpgrades;
    public int healthRegen;
    public int invincibilityFrame;
    public int critChance;
    public int critDamage;
    public int soulsAttract;
    public int luck;
    public string WhichGameMode;
    //---- SETTINGS ----
    //FPS
    public int maxFps;
    // Audio Volume
    public float globalVolume;
    public float musicVolume;
    public float sfxVolume;
    public bool IsAutoShoot;
    public bool IsAutoAim;

    //Carac
    public int playerHealth;
    public float playerDamages;
    public float playerAttackSpeed;
    public float playerMoveSpeed;
    public int playerRegen;
    public float playerFrame;
    public int playerCritchance;
    public float playerCritDamages;
    public int playerSoulsA;
    public int playerLuck;
    //Invoc
    public bool HasInvoc5;
    public bool HasInvoc6;
    public bool HasInvoc7;
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
    public bool FirstFight;
    //Special
    public bool HasSpecial1;
    public bool HasSpecial2;
    public bool HasSpecial3;
    public string WhichSpecial;
    //XP
    public int Lvl;
    public float Xp;
    //Difficulty
    public bool GeneralDifficulty;
    public bool EnemyAmountDifficulty;
    public bool XpAmountDifficulty;
    public bool RerollDifficulty;
    public bool NoDashDifficulty;
    public bool NoChestDifficulty;
    public bool NoInvocDifficulty;
    public bool NoDrop;
    public float PercentageBonus;

}
