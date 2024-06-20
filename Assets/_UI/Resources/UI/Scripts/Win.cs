using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Win : UICanvas
{
    public TMP_Text coinObtain;
    private void OnEnable()
    {
        coinObtain.text = LevelManager.instance.GetCoinObtainCurrentLevel().ToString();
        DataManager.ins.SetCurrentLevel(DataManager.ins.GetCurrentLevel() + 1);
        Time.timeScale = 0f;
    }
    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        UIManager.Ins.OpenUI<MianMenu>();
        Close(0);
        
        LevelManager.instance.OnLoadLevel(DataManager.ins.GetCurrentLevel());
    }
    public void ContinueButton()
    {
        Close(0);
        UIManager.Ins.OpenUI<GamePlay>();
        LevelManager.Ins.OnLoadLevel(DataManager.ins.GetCurrentLevel());
        MapController.instance.SetPlayingStateForEnemy(true);
        CameraFollow.instance.SetCameraOnPlay();
        Time.timeScale = 1f;
    }
}
