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

    public GameObject Enemy;
    public GameObject Boss;
    public Transform SpawnPosition;

    private int stage;
    private int level;

    private void Start()
    {
        stage = 1;
        level = 1;
        SpawnEnemy();
    }

    bool timer = false;
    float t = 0;

    private void Update()
    {
        if(timer == true)
        {
            t += Time.deltaTime;
            if (t > 1)
                LevelUp();
        }
    }

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
                StageUp();
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

    private void EndGame()
    {
        print("gj");
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(Enemy, SpawnPosition.position, Quaternion.identity);
        enemy.GetComponent<Enemy>().multiplier = MultiplierCalculator(stage, level);
    }

    private void SpawnBoss()
    {
        Instantiate(Boss, SpawnPosition.position, Quaternion.identity);
    }

    private float MultiplierCalculator(int Stage, int Level)
    {
        float a = Level / 10f;
        float multiplier = stage + a;
        return multiplier;
    }

    public void ForcedLevelUp()
    {
        GameObject enemy = FindObjectOfType<Enemy>().gameObject;
        Destroy(enemy);
    }

    #region PROPERTIES

    public int Stage => stage;

    public int Level => level;

    #endregion
}
