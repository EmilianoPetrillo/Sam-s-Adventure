using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject[] Enemy;
    private int enemyIndex;
    public GameObject[] Boss;
    public Transform SpawnPosition;

    private int stage;
    private int level;

    bool timer = false;
    float t = 0;

    private void Start()
    {
        stage = 1;
        level = 1;
    }


    private void Update()
    {
        if(timer == true)
        {
            t += Time.deltaTime;
            if (t > 1)
                LevelUp();
        }
    }

    #region STAGES AND LEVELS

    public void OnPlayerDeath()
    {
        MagicDice enemyProjectile = FindObjectOfType<MagicDice>();
        if(enemyProjectile != null)
            Destroy(enemyProjectile.gameObject);
        Enemy enemy = FindObjectOfType<Enemy>();
        if(enemy != null)
            Destroy(enemy.gameObject);
    }

    public void StartStuff()
    {
        SpawnEnemy();
    }

    bool firstBossKilled = false;

    public void LevelUp()
    {
        if (timer == false)
        {
            timer = true;
        }
        else if(timer == true && t > 1)
        {
            if (level >= 10)
            {
                if (firstBossKilled)
                    EndGame();
                else
                {
                    i++;//to know which boss has to be spawned at the next stage
                    UIController.Instance.level9Completed = false;
                    StageUp();
                }
            }
            else if (level == 9)
            {
                level++;
                UIController.Instance.level9Completed = true;
                SpawnBoss();
            }
            else if (level <= 8)
            {
                level++;
                SpawnEnemy();
            }
            timer = false;
            t = 0;
        }
    }
    
    public void StageUp()
    {
        firstBossKilled = true;
       /* if (stage >= 10)
        {
            EndGame();
        }*/
       // else
       // {
        stage++;
        level = 1;
        SpawnEnemy();
       // }
    }

    public void ForcedLevelUp()
    {
        GameObject enemy = FindObjectOfType<Enemy>().gameObject;
        Destroy(enemy);
        LevelUp();
    }

    public void ForcedLevelDown()
    {
        Player.Instance.PlayerRespawn();
        Enemy enemy = FindObjectOfType<Enemy>();
        if(enemy != null)
            Destroy(enemy.gameObject);
        if (level <= 10 && level > 1)
        {
            level -= 1;
            SpawnEnemy();
        }
        else if(level == 1)
        {
            SpawnEnemy();
        }
    }

    private void EndGame()
    {
        UIController.Instance.EndGamePanelStuff();
        Player.Instance.gameObject.SetActive(false);
    }
    #endregion

    #region ENEMIES SPAWN
    public void SpawnEnemy()
    {
        enemyIndex = Random.Range(0, Enemy.GetLength(0));
        GameObject enemy = Instantiate(Enemy[enemyIndex], SpawnPosition.position, Quaternion.identity);
        enemy.GetComponent<Enemy>().EnemySO.multiplier = MultiplierCalculator(stage, level);
    }

    private int i = 0;

    private void SpawnBoss()
    {
        GameObject boss = Instantiate(Boss[i], SpawnPosition.position, Quaternion.identity);
        boss.GetComponent<Enemy>().EnemySO.multiplier = MultiplierCalculator(stage, level);
    }

    private float MultiplierCalculator(int Stage, int Level)
    {
        float a = Level / 10f;
        float multiplier = 2 * stage + a;
        return multiplier;
    }
    #endregion

    #region PROPERTIES

    public int Stage => stage;

    public int Level => level;

    #endregion
}
