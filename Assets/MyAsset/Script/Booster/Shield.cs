using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectShield : GameUnit
{
    private Character owner;
    public void OnInit(Character character)
    {
        TF.localScale = Vector3.one * 4;
        SetOwner(character);
    }
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    public void SetOwner(Character owner)
    {
        this.owner = owner;
    }
    public Character GetOwner()
    {
        return this.owner;
    }
}
