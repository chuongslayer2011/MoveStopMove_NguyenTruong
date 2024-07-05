using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PantSkinData")]
public class PantData : ScriptableObject
{


    [Header("PantSkin")]
    [SerializeField] Material[] pantSkins;

    [Header("BodyColor")]
    [SerializeField] Material[] bodyColors;

    [Header("Hair")]
    [SerializeField] GameObject[] hairPrefabs;
    [Header("Shield")] 
    [SerializeField] GameObject[] shieldPrefabs;
    
    public Material GetMaterial(PantSkin pantSkin)
    {
        return pantSkins[(int)pantSkin];
    }
    public Material GetBodyColor(BodyColor bodyColor)
    {
        return bodyColors[(int) bodyColor];
    }
    public GameObject GetHair(Hair hair)
    {
        return hairPrefabs[(int)hair];
    }
    public GameObject GetShield(Shield shield)
    {
        return shieldPrefabs[(int)shield];
    }
}
public enum PantSkin
{
    none = 0,
    Batman = 1,
    chambi = 2,
    comy = 3,
    dabao = 4,
    onion = 5,
    pokemon = 6,
    rainbow = 7,
    skull = 8,
    vantim = 9,
}
public enum BodyColor
{
    blue = 0,
    golden = 1,
    grey = 2,
    pink  = 3,
    purple = 4,
    red = 5,
    white = 6,
}
public enum Hair
{
    arrow = 0,
    cowboy = 1,
    crown = 2,
    ear = 3,
    hat = 4,
    hat_cap = 5,
    hat_yellow = 6,
    headphone = 7,
    horn = 8,
    rau = 9,
    none = 10,
    witch = 11,
}
public enum Shield
{
    
    captain = 0,
    ShieldBasic = 1,
    none = 2,
    book = 3,
}