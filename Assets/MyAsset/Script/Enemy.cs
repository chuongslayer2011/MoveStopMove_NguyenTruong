using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private GameObject isTargetedMark;
    
    public override void OnInit()
    {   
        base.OnInit();
        int random = Random.Range(1, 10);
        this.Pant.material= pantData.GetMaterial((PantSkin)random);
        random = Random.Range(0, 7);
        this.bodySkin.material = pantData.GetBodyColor((BodyColor)random);
        this.targetIndicator.SetName(Const.GetRandomName(), this.bodySkin.material.color);
        this.weaponOnHand.SetWeaponOnHand((int)RandomPoolType());
        random = Random.Range(0, 10);
        this.hairOnHead.PutHairOn((Hair) random);
        this.transform.localScale = Random.Range(1, 1.5f) * Vector3.one;
        this.agent.enabled = true;
        isTargetedMark.SetActive(false);
        

    }

    public void SetTargetMark(bool isTargeted)
    {
        isTargetedMark.SetActive(isTargeted);
    }
    public PoolType RandomPoolType()
    {

        int random = UnityEngine.Random.Range(0, 4);
        return (PoolType)random;
    }
}
