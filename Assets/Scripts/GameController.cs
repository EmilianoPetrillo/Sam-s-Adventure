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

    public GameObject[] EnemiesWorld1;
    public GameObject[] EnemiesWorld2;
    public GameObject[] EnemiesWorld3;
    public GameObject[] EnemiesWorld4;
    public GameObject FakeEnemy;
    private int enemyIndex;
    public GameObject[] Boss;
    public Transform SpawnPosition;
    public GameObject Game;

    private int stage;
    private int level;
    public int maxLevel;

    bool timer = false;
    float t = 0;

    private void Start()
    {
        stage = 1;
        level = 1;
        maxLevel = 10;
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
        DestroyEnemy();
    }

    public void StartStuff()
    {
        SpawnFakeEnemy();
    }

    public void LevelUp()
    {
        if (timer == false)
        {
            timer = true;
        }
        else if(timer == true && t > 1)
        {
            Player.Instance.PlayerRespawn();
            if (level >= maxLevel)
            {
                StageUp();
                UIController.Instance.level9Completed = false;
            }
            else if (level == maxLevel - 1 && !UIController.Instance.level9Completed)
            {
                level++;
                UIController.Instance.level9Completed = true;
            }
            else if (level == maxLevel - 1 && UIController.Instance.level9Completed)
            {

            }
            else if (level <= maxLevel - 2)
            {
                level++;
            }
            timer = false;
            t = 0;
            SpawnEnemy();
        }
    }
    
    public void StageUp()
    {
        if (stage >= 10)
        {
            EndGame();
        }
        else
        {
            stage++;
            i++;
            level = 1;
            if (stage == 10)
                maxLevel = 30;
            if ((stage - 1) % 3 == 0)
                UITextController.Instance.ChangeGamePanel();
        }
    }

    public void ForcedLevelUp()
    {
        UIController.Instance.level9Completed = false;
        DestroyEnemy();
        LevelUp();
    }

    public void ForcedLevelDown()
    {
        DestroyEnemy();
        if (level == 10)
            level -= 1;
        SpawnEnemy();
    }

    private void EndGame()
    {
        UIController.Instance.EndGamePanelStuff();
        Player.Instance.gameObject.SetActive(false);
    }

    #endregion

    #region ENEMIES SPAWN

    public int StartingIndexToSpawnEnemy;

    public void SpawnEnemy()
    {
        if (level == maxLevel)
            SpawnBoss();
        else
        {
            if (stage <= 3)
            {
                enemyIndex = Random.Range(StartingIndexToSpawnEnemy, EnemiesWorld1.GetLength(0));
                GameObject enemy = Instantiate(EnemiesWorld1[enemyIndex], SpawnPosition.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().EnemySO.multiplier = MultiplierCalculator(stage, level);
            }
            else if (stage > 3 && stage <= 6)
            {
                enemyIndex = Random.Range(StartingIndexToSpawnEnemy, EnemiesWorld2.GetLength(0));
                GameObject enemy = Instantiate(EnemiesWorld2[enemyIndex], SpawnPosition.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().EnemySO.multiplier = MultiplierCalculator(stage, level);
            }
            else if (stage <= 9)
            {
                enemyIndex = Random.Range(StartingIndexToSpawnEnemy, EnemiesWorld3.GetLength(0));
                GameObject enemy = Instantiate(EnemiesWorld3[enemyIndex], SpawnPosition.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().EnemySO.multiplier = MultiplierCalculator(stage, level);
            }
            else if (stage == 10)
            {
                enemyIndex = Random.Range(StartingIndexToSpawnEnemy, EnemiesWorld4.GetLength(0));
                GameObject enemy = Instantiate(EnemiesWorld4[enemyIndex], SpawnPosition.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().EnemySO.multiplier = MultiplierCalculator(stage, level);
            }
        }
    }

    private int i = 0;

    private void SpawnBoss()
    {
        GameObject boss = Instantiate(Boss[i], SpawnPosition.position, Quaternion.identity);
        boss.GetComponent<Enemy>().EnemySO.multiplier = MultiplierCalculator(stage, level);
    }

    public void DestroyEnemy()
    {
        MagicDice enemyProjectile = FindObjectOfType<MagicDice>();
        if (enemyProjectile != null)
            Destroy(enemyProjectile.gameObject);
        Enemy enemy = FindObjectOfType<Enemy>();
        if (enemy != null)
            Destroy(enemy.gameObject);
        timer = false;
        t = 0;
    }

    private float MultiplierCalculator(int Stage, int Level)
    {
        float a = Level / 10f;
        float multiplier = Mathf.Pow(2, stage + a);
        return multiplier;
    }

    public void SpawnFakeEnemy()
    {
        Instantiate(FakeEnemy, SpawnPosition.position, Quaternion.identity);
    }

    #endregion

    #region PROPERTIES

    public int Stage => stage;

    public int Level => level;

    #endregion
}
