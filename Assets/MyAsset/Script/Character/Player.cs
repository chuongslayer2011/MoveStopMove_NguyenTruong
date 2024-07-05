using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : Character
{   
    
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private Transform shieldOnHand;
    private GameObject currentShieldOnHand;
    
    private void FixedUpdate()
    {   
        if (!this.targetable || !isPlay)
        {
            return;
        }
        Moving();
    }
    
    public override void OnInit()
    {
        base.OnInit();
        this.weaponOnHand.SetWeaponOnHand(DataManager.ins.GetWeaponOnHandID());
        this.hairOnHead.PutHairOn((Hair)DataManager.ins.GetCurrentHairID());
        this.PutPantOnByID(DataManager.ins.GetCurrentPantID());
        this.PutShieldOn(DataManager.ins.GetCurrentShield());
        this.transform.localScale = Vector3.one;
        ChangeAnim(Const.ILDE_ANIM);
        SetIsPlay(false);
        this.targetIndicator.SetName("You", Color.yellow);
    }
    public override void Moving()
    {
        
        Vector3 movementDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        if (movementDirection.sqrMagnitude > 0)
        {
            ChangeAnim(Const.RUN_ANIM);
            rb.velocity = movementDirection * movingSpeed + rb.velocity.y * Vector3.up;
            transform.forward = movementDirection;
        }
        else
        {
            if (!this.CheckTargetInRange())
                ChangeAnim(Const.ILDE_ANIM);
            this.rb.velocity = Vector3.zero;
        }
    }
    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        if (collider.CompareTag(Const.ENEMY_TAGNAME))
        {   
            collider.GetComponent<Enemy>().SetTargetMark(true);
        }
    }
    protected override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
        if (collider.CompareTag(Const.ENEMY_TAGNAME))
        {
            collider.GetComponent<Enemy>().SetTargetMark(false);
        }
    }
    public void EquipWeaponById(int weaponId)
    {
        this.weaponOnHand.SetWeaponOnHand(weaponId);
        DataManager.ins.SetWeaponOnHandID(weaponId);
    }
    public void PutHairOnByID(int hairId)
    {
        this.hairOnHead.PutHairOn((Hair)hairId);
    }
    public void EquipHair(int hairId)
    {
        this.hairOnHead.PutHairOn((Hair) hairId);
        DataManager.ins.SetHairID(hairId);

    }
    public void PutPantOnByID(int pantId)
    {
        this.Pant.material = pantData.GetMaterial((PantSkin)pantId);
    }
    public void EquipPant(int pantId)
    {
        this.Pant.material = pantData.GetMaterial((PantSkin)pantId);
        DataManager.ins.SetPantID(pantId);
    }
    public void ResetSkin()
    {
        this.hairOnHead.PutHairOn((Hair)DataManager.ins.GetCurrentHairID());
        this.PutPantOnByID(DataManager.ins.GetCurrentPantID());
        this.PutShieldOn(DataManager.ins.GetCurrentShield());
    }
    public void PutShieldOn(int shieldId)
    {   
        
        if (currentShieldOnHand != null)
        {
            Destroy(currentShieldOnHand.gameObject);
        }
        if (shieldId == 2)
        {
            return;
        }
        currentShieldOnHand = Instantiate(pantData.GetShield((Shield)shieldId),shieldOnHand.transform);
    }
    public void EquipShield(int shieldId)
    {
        PutShieldOn(shieldId);
        DataManager.ins.SetShield(shieldId);
    }
    public void Unequip(ItemInShop item)
    {
        if (item.GetComponent<HairItemScript>())
            EquipHair(10);
        else if (item.GetComponent<PantItem>())
        {
            EquipPant(0);
        }
        else if (item.GetComponent<ShieldItem>())
        {
            EquipShield(2);
        }
        else if (item.GetComponent<FullSetItem>())
        {
            EquipShield(2);
            EquipHair(10);
            EquipPant(0);
            DataManager.ins.SetFullSet(1);
        }
    }
    
}
