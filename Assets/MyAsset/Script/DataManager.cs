
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager ins;

    private void Awake()
    {
        ins = this;
        DontDestroyOnLoad(gameObject);
    }
    public bool isLoaded = false;
    public PlayerData playerData;
    public const string PLAYER_DATA = "PLAYER_DATA";


    private void OnApplicationPause(bool pause) { SaveData(); }
    private void OnApplicationQuit() { SaveData(); }

    public void LoadData()
    {
        string d = PlayerPrefs.GetString(PLAYER_DATA, "");
        if (d != "")
        {
            playerData = JsonUtility.FromJson<PlayerData>(d);
        }
        else
        {
            playerData = new PlayerData();
        }
        isLoaded = true;
        //loadskin
        //load pet

    }

    public void SaveData()
    {
        if (!isLoaded) return;
        string json = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(PLAYER_DATA, json);
    }
    public int GetCoin()
    {

        return playerData.gold;
    }
    public void ObtainCoin()
    {
        playerData.gold++;
    }
    public int GetWeaponOnHandID()
    {
        return playerData.idWeaponOnHand;
    }
    public void SetWeaponOnHandID(int weaponID)
    {
        playerData.idWeaponOnHand = weaponID;
    }
    public void SetSkin(ItemInShop item)
    {
        if (item.GetComponent<HairItemScript>())
        {
            playerData.idHat = item.Id;
        }
        else if (item.GetComponent<PantItem>())
        {
            playerData.idPant = item.Id;
        }
        else if (item.GetComponent<ShieldItem>())
        {
            playerData.idShield = item.Id;
        }
        else if (item.GetComponent<FullSetItem>())
        {
            FullSetItem f = item.GetComponent<FullSetItem>();
            playerData.idHat = f.HairID;
            playerData.idShield = f.ItemID;
            playerData.idFullSet = item.Id;
        }
    }
    public bool CheckWeaponOwn(int idWeapon)
    {
        return playerData.OwningStateWeapon[idWeapon];
    }
    public void Shopping(int price,int weaponID)
    {
        playerData.gold -= price;
        playerData.OwningStateWeapon[weaponID] = true;

    }
    public void Shopping(ItemInShop item)
    {
        playerData.gold -= item.price;
        if (item.GetComponent<HairItemScript>())
        {
            playerData.status_Hat[item.Id] = true;
        }
        else if (item.GetComponent<PantItem>())
        {
            playerData.status_Pant[item.Id-1] = true;
        }
        if (item.GetComponent<ShieldItem>())
        {
            playerData.status_shield[item.Id] = true;
        }
        else if (item.GetComponent<FullSetItem>())
        {
            playerData.fullSet[item.Id] = true;
        }


    }
    public void SetHairID(int hairID)
    {
        playerData.idHat = hairID;

    }
    public void SetPantID(int pantID)
    {
        playerData.idPant = pantID;
    }
    public void SetShield(int shield)
    {
        playerData.idShield = shield;
    }
    public void SetFullSet(int f)
    {
        playerData.idFullSet = f;
    }
    public int GetCurrentHairID()
    {
        return playerData.idHat;
    }
    public int GetCurrentPantID()
    {
        return playerData.idPant;
    }
    public int GetCurrentShield()
    {
        return playerData.idShield;
    }
    public int GetCurrentSkinEquip(ItemInShop item)
    {
        if (item.GetComponent<HairItemScript>())
        {
            return playerData.idHat;
        }
        else if (item.GetComponent<PantItem>())
        {
            return playerData.idPant;
        }
        else if (item.GetComponent<ShieldItem>())
        {
            return playerData.idShield;
        }
        else if (item.GetComponent<FullSetItem>())
        {
            return playerData.idFullSet;
        }
        return -1;
    }
    public bool CheckSkinOwn(ItemInShop item)
    {
        if (item.GetComponent<HairItemScript>())
            return playerData.status_Hat[item.Id];
        else if (item.GetComponent<PantItem>())
        {
            return playerData.status_Pant[item.Id-1];
        }
        else if (item.GetComponent<ShieldItem>())
        {
            return playerData.status_shield[item.Id];
        }
        else if (item.GetComponent<FullSetItem>())
        {
            return playerData.fullSet[item.Id];
        }
        return false;
    }
    public void SetCurrentLevel(int level)
    {
        playerData.level = level;
    }
    public int GetCurrentLevel()
    {
        return playerData.level;
    }
}


[System.Serializable]
public class PlayerData
{
    [Header("--------- Game Setting ---------")]
    public bool isNew;
    public bool isMusic;
    public bool isSound;
    public bool isVibrate;
    public bool isNoAds;
    public int starRate;


    [Header("--------- Game Params ---------")]
    public int gold;
    public int level;
    public int idSkin;
    public int idPant;
    public int idHat;
    public int idShield;
    public int idWeaponOnHand;
    public int idFullSet;
    public bool[] OwningStateWeapon = { true, false, false, false};
    public bool[] status_Pant = { false, false, false, false, false, false };
    public bool[] status_Hat = { false, false, false, false, false, false};
    public bool[] status_shield = { false, false };
    public bool[] fullSet = { false };
}
