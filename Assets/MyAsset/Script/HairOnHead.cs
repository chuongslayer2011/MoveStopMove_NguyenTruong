using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairOnHead : MonoBehaviour
{
    [SerializeField] private PantData hairData;
    private GameObject currentHair;
    public void PutHairOn(Hair hair)
    {
        if (currentHair != null)
        {
            Destroy(currentHair.gameObject);
        }
        if (hair != Hair.none)
        {
            currentHair = Instantiate(hairData.GetHair(hair), this.transform);
        }
        
    }
}
