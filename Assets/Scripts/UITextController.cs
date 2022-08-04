using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextController : MonoBehaviour
{

    public static UITextController Instance;

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

    public GameObject[] TextsAndArrows;

    public GameObject GamePanel;
    public GameObject StartPanel;
    public GameObject[] Backgrounds;
    public GameObject CurrentBackground;
    private int i = 0;
    private int j = 0;

    private void Start()
    {

    }

    public void DeleteStartStuff()
    {
        Destroy(StartPanel.gameObject);
        GamePanel.gameObject.SetActive(true);
    }

    public void ChangeGamePanel()
    {
        if(CurrentBackground != null)
        {
            Destroy(CurrentBackground);
            i++;
        }
        CurrentBackground = Instantiate(Backgrounds[i], Backgrounds[i].transform.position, Quaternion.identity);
    }

    public void NextPhase()
    {
        TextsAndArrows[j].SetActive(false);
        if(j >= TextsAndArrows.GetLength(0) - 1)
        {
            GameController.Instance.SpawnEnemy();
            ChangeGamePanel();
        }
        else
        {
            j++;
            TextsAndArrows[j].SetActive(true);
        }
    }

}
