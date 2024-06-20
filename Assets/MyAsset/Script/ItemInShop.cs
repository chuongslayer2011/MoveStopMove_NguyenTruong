using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInShop : MonoBehaviour
{
    public int price;
    public int Id;
    public Image lockIcon;
    public void SetLockIcon(bool active)
    {
        lockIcon.gameObject.SetActive(active);
    }
}
