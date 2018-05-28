using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpawnControl : MonoBehaviour {

    public PlayerControl playerControl;
    public LevelControl levelControl;
    public Transform[] path1;
    public Transform[] path2;
    public Transform[] path3;
    public GameObject EnemyPrefab;
    public GameObject EnemyPrefab2;
    public GameObject EnemyPrefab3;
    public float spawnRate;
    public float DelayRate;


    private Transform[] selectpath;
    private List<GameObject> Wave1;
    private List<GameObject> Wave2;
    private List<GameObject> Wave3;
    private float spawnTimer;
    private float delayTimer;
    private int count;
    private int level;
    private int Countlevel1;
    private int Countlevel2;
    private int Countlevel3;

    //for UI
    public Image LevelTimer;
    public Text main_text;

    // Use this for initialization
    void Start() {
        Wave1 = new List<GameObject>();
        Wave2 = new List<GameObject>();
        Wave3 = new List<GameObject>();
        SpawnEnemy(8, 1, 1, Wave1);
        SpawnEnemy(6, 3, 1, Wave2);
        SpawnEnemy(4, 4, 2, Wave3);
        level = 1;
        Countlevel1 = 0;
        Countlevel2 = 0;
        Countlevel3 = 0;
        spawnTimer = 0;
        delayTimer = 0;
    }

    // Update is called once per frame
    void Update() {
        if (playerControl.health > 0)
        { 
            CheckEmpty();
            if (spawnTimer < 0)
            {
                LevelTimer.fillAmount = 0;
                if (level == 1)
                {
                    main_text.text = "Wave 1";
                    if (Time.time > delayTimer)
                    {
                        if (Countlevel1 < 10)
                        {
                            Wave1[Countlevel1].GetComponent<EnemyControl>().SetAlive(true);
                            delayTimer = Time.time + DelayRate;
                            Countlevel1++;
                        }
                        else
                        {
                            spawnTimer = spawnRate;
                            level++;
                        }
                    }
                }
                else if (level == 2)
                {
                    main_text.text = "Wave 2";
                    if (Time.time > delayTimer)
                    {
                        if (Countlevel2 < 10)
                        {
                            Wave2[Countlevel2].GetComponent<EnemyControl>().SetAlive(true);
                            delayTimer = Time.time + DelayRate;
                            Countlevel2++;

                        }
                        else
                        {
                            spawnTimer = spawnRate;
                            level++;
                        }
                    }
                }
                else if (level == 3)
                {
                    main_text.text = "Final Wave";
                    if (Time.time > delayTimer )
                    {
                        if (Countlevel3 < 10)
                        {
                            Wave3[Countlevel3].GetComponent<EnemyControl>().SetAlive(true);
                            delayTimer = Time.time + DelayRate;
                            Countlevel3++;

                        }
                        else
                        {
                            level++;
                        }
                    }
                }
            }
            else
            { 
                spawnTimer -= Time.deltaTime * TimeControl.timeMultiplier;
                LevelTimer.fillAmount = spawnTimer / spawnRate;
            }

            if (Countlevel3 == 10)
            {
                CheckWin();
            }
        }
    }
    private void CheckWin()
    {

        if(ListIsEmpty(Wave1) & ListIsEmpty(Wave2) & ListIsEmpty(Wave3))
        {
            levelControl.EndGame(true);
        }
    }

    private void CheckEmpty()
    {
        if (level == 2)
        {
            if (ListIsEmpty(Wave1))
            {
                spawnTimer = -1;
            }
        }
        else if(level == 3)
        {
            if(ListIsEmpty(Wave1) & ListIsEmpty(Wave2))
            {
                spawnTimer = -1;
            }
        }
    }

    private bool ListIsEmpty(List<GameObject> templist)
    {
        for(int i=0;i < templist.Count; i++)
        {
            if (templist[i].activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

    private void AddEnemy(string monsterType, bool alive, Transform[] path,List<GameObject> listenemy)
    {
        if (monsterType == "Enemy1")
        {
            GameObject temp = Instantiate(EnemyPrefab);
            temp.GetComponent<EnemyControl>().SetAlive(alive, path);
            listenemy.Add(temp);
        }
        else if(monsterType == "Enemy2")
        {
            GameObject temp = Instantiate(EnemyPrefab2);
            temp.GetComponent<EnemyControl>().SetAlive(alive, path);
            listenemy.Add(temp);
        }
        else if (monsterType == "Enemy3")
        {
            GameObject temp = Instantiate(EnemyPrefab3);
            temp.GetComponent<EnemyControl>().SetAlive(alive, path);
            listenemy.Add(temp);
        }
    }

    private void SpawnEnemy(int maxEnemy1, int maxEnemy2, int maxEnemy3, List<GameObject> listenemy)
    {
        int numEnemy1 = 0;
        int numEnemy2 = 0;
        int numEnemy3 = 0;
        count = 10;
        while (count > 0)
        {
            int random = UnityEngine.Random.Range(0, 10);
            if (random < 4)
            {
                selectpath = path1;
            }
            else if (random < 7)
            {
                selectpath = path2;
            }
            else
            {
                selectpath = path3;
            }

            if (numEnemy1 < maxEnemy1)
            {
                AddEnemy("Enemy1", false, selectpath, listenemy);
                numEnemy1++;
                count--;
            }
            else if (numEnemy2 < maxEnemy2)
            {
                AddEnemy("Enemy2", false, selectpath, listenemy);
                numEnemy2++;
                count--;
            }
            else if (numEnemy3 < maxEnemy3)
            {
                AddEnemy("Enemy3", false, selectpath, listenemy);
                numEnemy3++;
                count--;
            }
        }
    }
    
}
