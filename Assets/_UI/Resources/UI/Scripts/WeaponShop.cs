using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class WeaponShop : UICanvas
{
    public List<WeaponOnDisplay> listWeapon;
    public WeaponOnDisplay currentWeaponOnDisplay;
    public int currentWeaponOnDisplayIndex;
    public TMP_Text coinText;
    public TMP_Text weaponTittleText;
    public TMP_Text weaponAttribute;
    public TMP_Text equipStateText;
    public GameObject equipButton;
    public GameObject coinIcon;
    private void OnEnable()
    {          
        currentWeaponOnDisplayIndex = 0;
        coinText.text = DataManager.ins.GetCoin().ToString();
        UpdateCurrentDisplay();
    }
    public void NextButton()
    {
        if (currentWeaponOnDisplayIndex < listWeapon.Count - 1)
        {
            currentWeaponOnDisplayIndex++;
            UpdateCurrentDisplay();
        }
    }   
    public void PreviousButton()
    {
        if (currentWeaponOnDisplayIndex > 0)
        {
            currentWeaponOnDisplayIndex--;
            UpdateCurrentDisplay();
        }
    }
    public void UpdateCurrentDisplay()
    {
        if (currentWeaponOnDisplay != null)
        {
            currentWeaponOnDisplay.gameObject.SetActive(false);
        }
        currentWeaponOnDisplay = listWeapon[currentWeaponOnDisplayIndex];
        weaponTittleText.text = currentWeaponOnDisplay._name.ToUpper();
        weaponAttribute.text = currentWeaponOnDisplay.attribute.ToUpper();
        currentWeaponOnDisplay.gameObject.SetActive(true);
        UpDateEquipButton();
    }
    public void UpDateEquipButton()
    {   coinIcon.SetActive(false);
        Button but = equipButton.GetComponent<Button>();
        but.enabled = true;
        Image img = equipButton.GetComponent<Image>();
        img.color = Color.white;
        if (currentWeaponOnDisplay != null)
        {
           
           if(currentWeaponOnDisplayIndex == DataManager.ins.GetWeaponOnHandID())
           {
                equipStateText.text = "Equipped";
                img.color = Color.grey;
                but.enabled = false;
           }
           else 
           if (DataManager.ins.CheckWeaponOwn(currentWeaponOnDisplayIndex))
           {
                equipStateText.text = "Equip";
                
           }
           else
           {    
                if(DataManager.ins.GetCoin() < currentWeaponOnDisplay.price)
                {
                    img.color = Color.grey;
                    but.enabled = false;
                }
                else
                {
                    img.color = Color.green;
                }
                equipStateText.text = currentWeaponOnDisplay.price.ToString();
                coinIcon.SetActive(true);
           }
        }
    }
    public void BuyingWeapon()
    {
        if (DataManager.ins.GetCoin() < currentWeaponOnDisplay.price)
        {
            return;
        }
       
        DataManager.ins.Shopping(currentWeaponOnDisplay.price,currentWeaponOnDisplayIndex);
        UpDateEquipButton();
    }
    public void EquipButton()
    {
        if (equipStateText.text == "Equipped")
        {
            return;
        }
        else if (equipStateText.text == "Equip")
        {
            Player player = FindObjectOfType<Player>();
            player.EquipWeaponById(currentWeaponOnDisplayIndex);
            UpDateEquipButton();
        }
        else
        {
            BuyingWeapon();
            coinText.text = DataManager.ins.GetCoin().ToString();
        }
    }
    public void CloseButton()
    {
        Close(0);
        UIManager.Ins.OpenUI<MianMenu>();
    }
}
