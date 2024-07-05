using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WeaponOnHand : MonoBehaviour
{
    [SerializeField] private PoolType poolType;
    [SerializeField] private WeaponOnHandData weaponOnHandData;
    private GameObject currentWeaponOnHand;
    private Transform tf;
    public Transform TF
    {
        get
        {
            //tf = tf ?? gameObject.transform;
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }
    public void SetWeaponOnHand(int id)
    {
        if (currentWeaponOnHand != null)
        {
            Destroy(currentWeaponOnHand.gameObject);
        }
        this.poolType = (PoolType)id;
        currentWeaponOnHand = Instantiate(weaponOnHandData.GetModel(poolType), TF.position, TF.rotation, TF);
    }
    public void DestroyCurrentWeapon()
    {
        if (currentWeaponOnHand != null)
        {
            Destroy(currentWeaponOnHand.gameObject);
        }
    }
    public PoolType GetPoolType()
    {
        return this.poolType;
    }
    public void SetPoolType(PoolType poolType)
    {
        this.poolType = poolType;
    }
}
