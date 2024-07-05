using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantItem : ItemInShop
{
    [SerializeField] private PantSkin pant;
    private void OnEnable()
    {
        this.SetLockIcon(!DataManager.ins.CheckSkinOwn(this));
    }
    public int GetPant()
    {
        return (int)this.pant;
    }
}
