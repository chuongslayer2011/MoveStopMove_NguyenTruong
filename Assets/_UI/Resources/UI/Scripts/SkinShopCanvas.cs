using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopCanvas : UICanvas
{
    public ItemInShop currentSelectItem;
    public TMP_Text equipStateText;
    public GameObject equipButton;
    public GameObject coinIcon;
    public TMP_Text coinText;
    public GameObject hairPage;
    public GameObject pantPage;
    public GameObject shieldPage;
    public GameObject fullSetPage;
    public GameObject currentPage;
    private void OnEnable()
    {
        coinText.text = DataManager.ins.GetCoin().ToString();
        CameraFollow.instance.SetCameraOnSkinShop();
        equipButton.SetActive(false);
        HairPage();
    }
    public void CloseButton()
    {
        FindObjectOfType<Player>().ResetSkin();
        UIManager.Ins.OpenUI<MianMenu>();
        Close(0);
    }
    public void HairItemButton()
    {
        Player p = FindObjectOfType<Player>();
        p.PutHairOnByID(currentSelectItem.GetComponent<HairItemScript>().GetHairItem());
    }
    public void SetCurrentSelectHair(HairItemScript hair)
    {   
        ResetColor();
        this.currentSelectItem = hair;
        currentSelectItem.GetComponent<Image>().color = Color.green;
        UpDateEquipButton();
    }
    public void PantItemButton()
    {
        Player p = FindObjectOfType<Player>();
        p.PutPantOnByID(currentSelectItem.GetComponent<PantItem>().GetPant());
    }
    public void SetCurrentSelectPant(PantItem pant)
    {   
        ResetColor();
        this.currentSelectItem = pant;
        currentSelectItem.GetComponent<Image>().color = Color.green;
        UpDateEquipButton();
    }
    public void FullItemButton()
    {
        Player p = FindObjectOfType<Player>();
        FullSetItem f = currentSelectItem.GetComponent<FullSetItem>();
        p.PutHairOnByID(f.HairID);
        p.PutShieldOn(f.ItemID);
    }
    public void SetCurrentSelectSet(FullSetItem fullset)
    {
        ResetColor();
        this.currentSelectItem = fullset;
        currentSelectItem.GetComponent<Image>().color = Color.green;
        UpDateEquipButton();
    }
    public void ShieldItemButton()
    {
        Player p = FindObjectOfType<Player>();
        p.PutShieldOn(currentSelectItem.GetComponent<ShieldItem>().GetShield());
    }
    public void SetCurrentSelectShield(ShieldItem shield)
    {   
        ResetColor();
        this.currentSelectItem = shield;
        currentSelectItem.GetComponent<Image>().color = Color.green;
        UpDateEquipButton();
    }
    public void UpDateEquipButton()
    {
        equipButton.SetActive(true);
        coinIcon.SetActive(false);
        Button but = equipButton.GetComponent<Button>();
        but.enabled = true;
        Image img = equipButton.GetComponent<Image>();
        img.color = Color.white;
        if (this.currentSelectItem != null)
        {

            if (this.currentSelectItem.Id == DataManager.ins.GetCurrentSkinEquip(this.currentSelectItem))
            {
                equipStateText.text = "Unequip";
                img.color = Color.red;
            }
            else
            if (DataManager.ins.CheckSkinOwn(this.currentSelectItem))
            {
                equipStateText.text = "Equip";

            }
            else
            {
                if (DataManager.ins.GetCoin() <  this.currentSelectItem.price)
                {
                    img.color = Color.grey;
                    but.enabled = false;
                }
                else
                {
                    img.color = Color.green;
                }
                equipStateText.text = this.currentSelectItem.price.ToString();
                coinIcon.SetActive(true);
            }
        }
    }
    public void EquipButton()
    {
        if (equipStateText.text == "Unequip")
        {
            FindObjectOfType<Player>().Unequip(currentSelectItem);
            equipButton.SetActive(false);
            ResetColor();
            return;
        }
        else if (equipStateText.text == "Equip")
        {
            DataManager.ins.SetSkin(currentSelectItem);           
        }
        else
        {
            BuyingSkin();
            coinText.text = DataManager.ins.GetCoin().ToString();
        }
        UpDateEquipButton();
    }
    public void BuyingSkin()
    {
        if (DataManager.ins.GetCoin() < currentSelectItem.price)
        {
            return;
        }

        DataManager.ins.Shopping(currentSelectItem);
        currentSelectItem.SetLockIcon(!DataManager.ins.CheckSkinOwn(currentSelectItem));
        
    }
    public void HairPage()
    {
        CloseCurrentPage();
        currentPage = hairPage;
        currentPage.SetActive(true);
    }
    public void PantPage()
    {
        CloseCurrentPage();
        currentPage = pantPage;
        currentPage.SetActive(true);
    }
    public void ShieldPage()
    {
        CloseCurrentPage();
        currentPage = shieldPage;
        currentPage.SetActive(true);
    }
    public void FullSetPage()
    {
        CloseCurrentPage();
        currentPage = fullSetPage;
        currentPage.SetActive(true);
    }
    public void CloseCurrentPage()
    {
        if (currentPage != null)
        {
            currentPage.SetActive(false);
        }
        equipButton.SetActive(false);
        FindObjectOfType<Player>().ResetSkin();
        ResetColor();
    }
    public void ResetColor()
    {
        if(currentSelectItem != null)
        {
            currentSelectItem.GetComponent<Image>().color =  Color.gray;
        }
    }
}

