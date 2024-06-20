using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponOnHandData")]
public class WeaponOnHandData : ScriptableObject
{
    
    

        [SerializeField] GameObject[] prefabs;
        public GameObject GetModel(PoolType poolType)
        {
            return prefabs[(int)poolType];
        }
        public GameObject GetModelById(int poolType)
        {
            return prefabs[poolType];
        }
}
