using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lose : UICanvas
{
    public static Lose Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        coinObtain.text = LevelManager.Ins.GetCoinObtainCurrentLevel().ToString();   
    }
    public TMP_Text coinObtain;

    public TMP_Text rankingText;
    public void SetRankingText()
    {
        rankingText.text = MapController.instance.EnemyAlive().ToString();
    }
    public void MainMenuButton()
    {
        UIManager.Ins.OpenUI<MianMenu>();
        CameraFollow.instance.SetCameraOnMainManu();
        LevelManager.instance.OnLoadLevel(DataManager.ins.GetCurrentLevel());
        Close(0);
        CameraFollow.instance.SetOffset(0, 3, -2);
    }
}
