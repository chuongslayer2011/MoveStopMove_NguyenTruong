using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairItemScript : ItemInShop
{
    [SerializeField] private Hair hairType;
    private void OnEnable()
    {
        this.SetLockIcon(!DataManager.ins.CheckSkinOwn(this));
    }
    public int GetHairItem()
    {
        return (int) this.hairType;
    }
}
