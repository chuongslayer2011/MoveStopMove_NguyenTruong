using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Weapon Data", order = 50)]
public class WeaponData : ScriptableObject
{
    public int id;
    public string itemName = "Weapon Name";
    public string description = "Weapon Description";
    public string price = "0";
    public Image icon;
    public GameObject prefab;
    public PoolType poolType;
}