using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{   
    public static LevelManager instance;
    [SerializeField] private MapController[] levels;
    [SerializeField] private List<int> enemyOnLevel = new List<int>();
    [SerializeField] private Player player;
    private MapController currentLevel;
    private int currentLevelIndex;
    private int coinObtainCurrentLevel=0;
    
    private void Awake()
    {
        instance = this;
    }
    public void Start()
    {   
        OnLoadLevel(DataManager.ins.GetCurrentLevel());
        OnInit();
    }

    public void OnInit()
    {
        player.OnInit();
        

    }
    public void OnReset()
    {
        
        OnLoadLevel(currentLevelIndex);
        player.OnInit();
    }

    //tao prefab level moi
    public void OnLoadLevel(int level)
    {
        if (currentLevel == null)
        {
            currentLevel = Instantiate(levels[0]);
        }
        currentLevel.SetEnemyOnMap(enemyOnLevel[level]);       
        currentLevel.OnInit();
        player.transform.position = currentLevel.GetStartingPoint();
        currentLevelIndex = level;
        player.OnInit();
        coinObtainCurrentLevel = 0;
    }
    public void ObtainCoinCurrentLevel()
    {
        coinObtainCurrentLevel++;
    }
    public int GetCoinObtainCurrentLevel()
    {
        return coinObtainCurrentLevel;
    }
    public Player GetPlayer()
    {
        return player;  
    }
}