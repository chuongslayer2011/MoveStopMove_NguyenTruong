using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    public TMP_Text aliveText;
    public static GamePlay instance;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        aliveText.text = (MapController.instance.EnemyAlive() + 1).ToString();
    }
    public void ChangeAliveText()
    {
        aliveText.text = (MapController.instance.EnemyAlive()+1).ToString();
    }
    public void SettingButton()
    {
        UIManager.Ins.OpenUI<Setting>();
        //Time.timeScale = 0;
        Close(0);
    }
}
