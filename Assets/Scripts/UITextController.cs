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
    public Image[] Backgrounds;
    private int i = 0;

    public void DeleteStartStuff()
    {
        Destroy(StartPanel.gameObject);
        GamePanel.gameObject.SetActive(true);
    }

    public void DeleteOtherStartStuff()
    {
        for(int i = 0; i < TextsAndArrows.GetLength(0); i++)
        {
            Destroy(TextsAndArrows[i]);
        }
    }

    public void ChangeGamePanel()
    {
        i++;
        Image image = GamePanel.GetComponent<Image>();
        image.sprite = Backgrounds[i].sprite;
    }

}
