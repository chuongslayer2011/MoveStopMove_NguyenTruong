using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class MapController : MonoBehaviour
{
    public static MapController instance;
    [SerializeField]private List<Character> currentEnemyOnMap = new List<Character>();
    [SerializeField] private Transform startingPoint;
    private List<Booster> currentBoosterOnMap = new List<Booster>();
    private int enemyAmountOnMap;
    private Dictionary<Collider, Character> characterOnMap = new Dictionary<Collider, Character>();
    [SerializeField] private GameObject frencePrefab;
    private void Awake()
    {
        instance = this;
    }
    public void OnInit()
    {
        Clear();
        SpawnAllEnemy();
        //SpawnDecor();
        for (int i = 0; i < 2; i++)
        {
            SpawnBooster();
        }
        Player p = FindObjectOfType<Player>();
        characterOnMap.Add(p.CLD, p);
    }
    public Vector3 RandomSpawnPoint()
    {
        Vector2 randomPosition = Random.insideUnitCircle * 50;
        Vector3 spawnPoint = new Vector3(randomPosition.x, 1.5f, randomPosition.y);
        return spawnPoint;
    }
    public void SpawnAllEnemy()
    {
        for (int i = 0; i <= 10; i++)
        {

            SpawnEnemy(false);
        }
    }
    public void SpawnDecor()
    {
        for(int i = 0; i <= 25; i++)
        {
            Vector3 decor1 = new Vector3(-38.5f, 0.5f, -53.5f + 4.5f * i);
            Vector3 decor2 = new Vector3(-38.5f + 100f, 0.5f, -53.5f + 4.5f * i);
            GameObject g1 = Instantiate(frencePrefab,decor1,Quaternion.Euler(0,0,0));
            GameObject g2 = Instantiate(frencePrefab, decor2, Quaternion.Euler(0, 0, 0));
            g1.transform.SetParent(this.transform);
            g2.transform.SetParent(this.transform);
        }
        for (int i = 0; i <= 22; i++)
        {
            Vector3 decor1 = new Vector3(-51.5f + 4.5f * i, 0.5f, -48.5f);
            Vector3 decor2 = new Vector3(-51.5f + 4.5f * i, 0.5f, -48.5f + 120f);
            GameObject g3 = Instantiate(frencePrefab, decor1, Quaternion.Euler(0, -90, 0));
            GameObject g4 = Instantiate(frencePrefab, decor2, Quaternion.Euler(0, -90, 0));
            g3.transform.SetParent(this.transform);
            g4.transform.SetParent(this.transform);
        }
    }
    public void SetPlayingStateForEnemy(bool playing)
    {
        for (int i = 0; i < currentEnemyOnMap.Count; i++)
        {
            currentEnemyOnMap[i].SetIsPlay(playing);
        }
    }
    public Vector3 GetStartingPoint()
    {
        return this.startingPoint.position;
    }
    public void RemoveEnemyOnMap(Character character)
    {
        if (character == null) return;      
        currentEnemyOnMap.Remove(character);
        characterOnMap.Remove(character.CLD);
        enemyAmountOnMap--;
        GamePlay.instance.ChangeAliveText();      
        if(enemyAmountOnMap > currentEnemyOnMap.Count)
        {
            SpawnEnemy(true);
        }
        
    }
    private void Clear()
    {
        for (int i = 0; i < currentEnemyOnMap.Count; i++)
        {   
            if (currentEnemyOnMap[i] != null)
            {
                currentEnemyOnMap[i].OnDespawn();
                SimplePool.Despawn(currentEnemyOnMap[i]);
            }

            
        }
        for (int i = 0;i < currentBoosterOnMap.Count; i++)
        {
            if (currentBoosterOnMap[i] != null)
                SimplePool.Despawn(currentBoosterOnMap[i]);
        }
        currentEnemyOnMap.Clear();
        currentBoosterOnMap.Clear();
        characterOnMap.Clear();
    }
    public int EnemyAlive()
    {
        return enemyAmountOnMap;
    }
    public void SpawnBooster()
    {   
        Vector3 offset = new Vector3(0,-1f,0);
        Booster b = SimplePool.Spawn<Booster>(PoolType.booster, RandomSpawnPoint() + offset, Quaternion.Euler(0, 0, 0));
        currentBoosterOnMap.Add(b);
        b.OnInit();
    }
    public void SpawnEnemy(bool isPlay)
    {

        Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Enemy, RandomSpawnPoint(), Quaternion.Euler(0, 0, 0));
        currentEnemyOnMap.Add(enemy);
        characterOnMap.Add(enemy.CLD, enemy);
        enemy.OnInit();
        enemy.SetIsPlay(isPlay);
    }
    public Character GetCharacterByCollider(Collider collider)
    {   
        if (characterOnMap.ContainsKey(collider))
        return characterOnMap[collider];
        return null;
    }
    public void SetEnemyOnMap(int amout)
    {
        this.enemyAmountOnMap = amout;
    }
}
