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
    public GameObject Boss;
    public Transform SpawnPosition;

    private int stage;
    private int level;

    bool timer = false;
    float t = 0;

    private void Start()
    {
        stage = 1;
        level = 1;
        SpawnEnemy();
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
                UIController.Instance.level9Completed = false;
                //StageUp();
                EndGame();
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
        if(stage >= 10)
        {
            EndGame();
        }
        else
        {
            stage++;
            level = 1;
            SpawnEnemy();
        }
    }

    public void ForcedLevelUp()
    {
        GameObject enemy = FindObjectOfType<Enemy>().gameObject;
        Destroy(enemy);
    }

    public void ForcedLevelDown()
    {
        Player.Instance.PlayerRespawn();
        GameObject enemy = FindObjectOfType<Enemy>().gameObject;
        Destroy(enemy);
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
    private void SpawnEnemy()
    {
        enemyIndex = Random.Range(0, Enemy.GetLength(0));
        GameObject enemy = Instantiate(Enemy[enemyIndex], SpawnPosition.position, Quaternion.identity);
        enemy.GetComponent<Enemy>().EnemySO.multiplier = MultiplierCalculator(stage, level);
    }

    private void SpawnBoss()
    {
        GameObject boss = Instantiate(Boss, SpawnPosition.position, Quaternion.identity);
        boss.GetComponent<Enemy>().EnemySO.multiplier = MultiplierCalculator(stage, level);
    }

    private float MultiplierCalculator(int Stage, int Level)
    {
        float a = Level / 10f;
        float multiplier = stage + a;
        return multiplier;
    }
    #endregion
    #region PROPERTIES

    public int Stage => stage;

    public int Level => level;

    #endregion
}
