using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum gameStatus
{
    next,play,gameover,win
};
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private int totalWaves = 10;
    [SerializeField]
    private Text totalMoneyLbl;
    [SerializeField]
    private Text curretWaveLbl;
    [SerializeField]
    private Text totalEscapedLbl;
    [SerializeField] 
    private GameObject spawnPoint;
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private int maxEnemiesOnScreen;
    [SerializeField]
    private int totalEnemies;
    [SerializeField]
    private int enemiesPerSpawn;
    [SerializeField]
    private Text playBtnLbl;
    [SerializeField]
    private Button playBtn;

    private int waveNumber = 0;
    private int totalMoney = 10;
    private int totalEscaped = 0;
    private int roundEscaped = 0;
    private int totalKilled = 0;
    private int whichEnemiesToSpawn = 0;
    private gameStatus currentState = gameStatus.play;
    public List<Enemy> EnemyList = new List<Enemy>();
/*    private int enemiesOnScreen = 0;*/
    const float spawDelay = 0.5f;

    public int TotalEscaped { 
        get { return totalEscaped; }
        set { roundEscaped = value; }
    }

    public int RoundEscaped { 
        get { return roundEscaped; }
        set { roundEscaped = value; }
    }

    public int TotalKilled { 
        get { return totalKilled; }
        set { totalKilled = value; }
    }
    public int TotalMoney
    {
        get { return totalMoney; }
        set { 
            totalMoney = value;
            totalMoneyLbl.text = totalMoney.ToString();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        /*  StartCoroutine(spawn());*/
        playBtn.gameObject.SetActive(false);
        showMenu();
    }
/*    void Update()
    {
       *//* spawn();*//*
       handleEscape();
    }*/


    IEnumerator spawn()
    {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                //enemiesOnScreen
                if (EnemyList.Count < maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                    /*enemiesOnScreen += 1;*/
                }
            }
            yield return new WaitForSeconds(spawDelay);
            StartCoroutine(spawn());
        }
        
    }

    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }
    public void UnregisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestroyAllEnemies()
    {
        foreach(Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);  
        }
        EnemyList.Clear();
    }
/*    public  void removeEnemyFromScreen()
    {
        if (enemiesOnScreen > 0)
            enemiesOnScreen -= 1;
    }*/

    public void addMoney(int amount)
    {
        TotalMoney += amount;
    }
    public void subtractMoney(int amount)
    {
        TotalMoney -= amount;   
    }

    public void isWaveOver()
    {
        totalEscapedLbl.text = "Escaped " + TotalEscaped + "/10";
        if((RoundEscaped + TotalEscaped) == totalEnemies)
        {
            setCurrentGameState();
            showMenu();

        } 
    }

    public void setCurrentGameState()
    {
        if(TotalEscaped >= 10)
        {
            currentState = gameStatus.gameover;

        }else if (waveNumber == 0 && (TotalKilled + RoundEscaped) == 0)
        {
            currentState = gameStatus.play;
        }else if(waveNumber >= totalWaves)
        {
            currentState = gameStatus.win;
        }
        else
        {
            currentState = gameStatus.next;
        }
    }

    public void showMenu()
    {
        switch(currentState)
        {
            case gameStatus.gameover:
                playBtnLbl.text = "Play Again";
                break;
            case gameStatus.next:
                playBtnLbl.text = "Next Wave";
                break;
            case gameStatus.win:
                playBtnLbl.text = "Play";
                break;
            case gameStatus.play:
                playBtnLbl.text = "Play";
                break;
        }
        playBtn.gameObject.SetActive(true);

    }

    public void playBtnPressed()
    {
        switch (currentState)
        {
            case gameStatus.next:
                waveNumber += 1;
                totalEnemies += waveNumber;
                break;
            default:
                totalEnemies = 3;
                TotalEscaped = 0;
                TotalMoney = 10;
                totalMoneyLbl.text = TotalMoney.ToString();
                totalEscapedLbl.text = "Escaped " + TotalEscaped + "/10";
                break;
        }
        DestroyAllEnemies();
        TotalKilled = 0;
        RoundEscaped = 0;
        curretWaveLbl.text = "Wave " + (waveNumber + 1);
        StartCoroutine(spawn());
        playBtn.gameObject.SetActive(false);
    }
    //delete tower
/*    private void handleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TowerManager.Instance.disnableDragSprite();
            TowerManager.Instance.towerBtnPressed = null;
        }    
    }*/
}
