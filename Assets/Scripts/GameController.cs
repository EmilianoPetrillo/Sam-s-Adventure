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
    public GameObject FakeEnemy;
    private int enemyIndex;
    public GameObject[] Boss;
    public Transform SpawnPosition;

    private int stage;
    private int level;
    private int maxLevel;

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
            if (level >= maxLevel)
            {
                StageUp();
                UIController.Instance.level9Completed = false;
            }
            else if (level == maxLevel - 1 && !UIController.Instance.level9Completed)
            {
                level++;
                UIController.Instance.level9Completed = true;
                SpawnBoss();
            }
            else if (level == maxLevel - 1 && UIController.Instance.level9Completed)
            {
                SpawnEnemy();
            }
            else if (level <= maxLevel - 2)
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
        if (stage >= 10)
        {
            EndGame();
        }
        else
        {
            stage++;
            level = 1;
            SpawnEnemy();
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
    public void SpawnEnemy()
    {
        enemyIndex = Random.Range(0, Enemy.GetLength(0));
        GameObject enemy = Instantiate(Enemy[enemyIndex], SpawnPosition.position, Quaternion.identity);
        enemy.GetComponent<Enemy>().EnemySO.multiplier = MultiplierCalculator(stage, level);
    }

    private void SpawnBoss()
    {
        GameObject boss = Instantiate(Boss[Random.Range(0, Boss.GetLength(0))], SpawnPosition.position, Quaternion.identity);
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
    }

    private float MultiplierCalculator(int Stage, int Level)
    {
        float a = Level / 10f;
        float multiplier = 2 * stage + a;
        return multiplier;
    }

    public void SpawnFakeEnemy()
    {
        Instantiate(FakeEnemy, SpawnPosition.position, Quaternion.identity);
    }

    public void FakeEnemyDeath()
    {
        DestroyEnemy();
        SpawnEnemy();
        UITextController.Instance.DeleteOtherStartStuff();
    }

    #endregion

    #region PROPERTIES

    public int Stage => stage;

    public int Level => level;

    #endregion
}
