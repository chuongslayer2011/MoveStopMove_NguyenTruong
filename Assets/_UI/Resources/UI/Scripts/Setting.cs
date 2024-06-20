using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{
    public void ContinueButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        Time.timeScale = 1;
        Close(0);
    }
    public void HomeButton()
    {   
        Time.timeScale = 1;
        UIManager.Ins.OpenUI<MianMenu>();
        Close(0);
        CameraFollow.instance.SetCameraOnMainManu();
        LevelManager.instance.OnLoadLevel(DataManager.ins.GetCurrentLevel());
    }
}
