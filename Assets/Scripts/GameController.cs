using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
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

    public int Stage
    {
        get { return stage; }
    }
    public int Level
    {
        get { return level; }
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
                StageUp();
            }
            else if (level == 9)
            {
                level++;
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
        Instantiate(Enemy, SpawnPosition.position, Quaternion.identity);
    }

    private void SpawnBoss()
    {
        Instantiate(Boss, SpawnPosition.position, Quaternion.identity);
    }
}
