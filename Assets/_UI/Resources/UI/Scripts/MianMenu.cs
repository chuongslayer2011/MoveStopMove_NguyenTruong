using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MianMenu : UICanvas
{
    public TMP_Text coinText;
    private void OnEnable()
    {   

        coinText.text = DataManager.ins.GetCoin().ToString();
        CameraFollow.instance.SetCameraOnMainManu();
    }
    public void PlayButton()
    {  
        UIManager.Ins.OpenUI<GamePlay>();
        Close(0);
        CameraFollow.instance.SetCameraOnPlay();
        FindObjectOfType<Player>().SetIsPlay(true);
        MapController.instance.SetPlayingStateForEnemy(true);
    }
    public void WeaponButton()
    {
        UIManager.Ins.OpenUI<WeaponShop>();
        Close(0);
    }
    public void SkinButton()
    {
        UIManager.Ins.OpenUI<SkinShopCanvas>();
        Close(0);
    }
}
