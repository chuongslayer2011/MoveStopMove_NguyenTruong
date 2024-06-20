using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullSetItem : ItemInShop
{
    public int HairID;
    public int ItemID;

    private void OnEnable()
    {
        this.SetLockIcon(!DataManager.ins.CheckSkinOwn(this));
    }

}
