using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<Bot> botPrefab;
    [SerializeField] private List<GameObject> bossPrefab;
    [SerializeField] private List<Bot> listAliveBot;
    [SerializeField] private List<List<Bot>> botChapterPrefab = new List<List<Bot>>();

    public Player player;
    [SerializeField] private Vector3 offsetSpawn;
    [SerializeField] private float sumEachTypeOfBot;
    [SerializeField] private float aliveBot;
    [SerializeField] private int currentChapter;

    [SerializeField] private bool isBossState;

    [SerializeField] private List<MapController> mapPrefab;
    [SerializeField] private MapController currentMap;

    private void Start()
    {
        LoadBotPrefabToBotChapterPrefab();
    }

    public void OnInit()
    {
        isBossState = false;
        aliveBot = 0f;
        currentChapter = 0;
        currentMap = Instantiate(mapPrefab[currentChapter],
                                 new Vector3(player.transform.position.x, 0, 0),
                                 transform.rotation);
        currentMap.OnInit();
    }

    private void Update()
    {
        //InfinityMap();

        if (GameManager.Instance.IsState(GameState.Gameplay) == false)
            return;

        if (isBossState)
        {
            if (aliveBot <= -7)
            {
                aliveBot = 0;
                player.levelPlayer++;
                isBossState = false;
            }
            return;
        }

        if (aliveBot <= 0f)
        {
            int l = (int)UnityEngine.Random.Range(0, 16);
            SpawnBot(l, l+3);
        }
        if (!isBossState && player.levelPlayer != 0 && player.levelPlayer % 5 == 0)
        {
            isBossState = true;
            BossSpawn((int)((player.levelPlayer-1)/5));
            UIManager.Instance.CallBossWarningUI();
        }
        if(player.levelPlayer >= 16 && currentChapter != 1)
        {
            ChangeNextChapter();
        }
    }

    private void ChangeNextChapter()
    {
        currentChapter = 1;
        currentMap.OnDespawn();
        currentMap = Instantiate(mapPrefab[currentChapter],
                                 new Vector3(player.transform.position.x, 0, 0),
                                 transform.rotation);
        currentMap.OnInit();
    }

    public void LoadBotPrefabToBotChapterPrefab()
    {
        List<Bot> tmp = new List<Bot>();
        for(int i=0;i<(int)botPrefab.Count;i++)
        {
            tmp.Add(botPrefab[i]);
            if(i == (int)(botPrefab.Count / 2))
            {
                botChapterPrefab.Add(tmp);
                tmp.Clear();
            }
        }
        botChapterPrefab.Add(tmp);
        Debug.Log(botChapterPrefab.Count);
        Debug.Log(botChapterPrefab[0].Count);
        Debug.Log(botChapterPrefab[1].Count);
    }

    public void InfinityMap()
    {
        if(!currentMap.isPlayerContaining)
        {
            currentMap.OnDespawn();
            currentMap = SimplePool.Spawn<MapController>(mapPrefab[currentChapter], 
                                     new Vector3(player.transform.position.x, 0, 0), 
                                     transform.rotation);
            currentMap.OnInit();
        }
    }

    public void ResetGame()
    {
        for(int i=0;i<listAliveBot.Count;i++)
        {
            listAliveBot[i].OnDespawn();
        }
        
        player.OnInit();
        OnInit();
    }

    #region Bot Actions

    public void SpawnBot(int type_l, int type_r)
    {
        SpawnBotLeft(type_l, type_r);
        SpawnBotRight(type_l, type_r);
    }
    
    public void SpawnBotLeft(int type_l, int type_r)
    {
        for(int type=type_l;type<type_r;type++)
        {
            for(int i=0;i<sumEachTypeOfBot;i++)
            {
                BotSpawn(type, player.transform.position - offsetSpawn);
            }
        }
    }

    public void SpawnBotRight(int type_l, int type_r)
    {
        for (int type = type_l; type < type_r; type++)
        {
            for (int i = 0; i < sumEachTypeOfBot; i++)
            {
                BotSpawn(type, player.transform.position + offsetSpawn);
            }
        }
    }

    public void InitBot(Bot bot)
    {
        listAliveBot.Add(bot);
        aliveBot++;
    }

    public void DespawnBot(Bot bot)
    {
        listAliveBot.Remove(bot);
        aliveBot--;
    }

    public void BotSpawn(int type, Vector3 position)
    {
        Bot bot = SimplePool.Spawn<Bot>(botChapterPrefab[currentChapter][type], position, transform.rotation);
        bot.OnInit();
    }

    public void BossSpawn(int type)
    {
        Instantiate(bossPrefab[type], transform.position - offsetSpawn, transform.rotation);
    }

    #endregion
}
