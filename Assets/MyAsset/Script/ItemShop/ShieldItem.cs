using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : ItemInShop
{
    [SerializeField] private Shield shield;
    private void OnEnable()
    {
        this.SetLockIcon(!DataManager.ins.CheckSkinOwn(this));
    }
    public int GetShield()
    {
        return (int)this.shield;
    }
}
