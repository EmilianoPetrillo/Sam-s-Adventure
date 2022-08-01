using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextController : MonoBehaviour
{

    public static UITextController Instance;

    private bool timer = false;
    private float t = 0;

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

    private void Update()
    {
        if (timer)
            t += Time.deltaTime;
        if (t >= 5)
            DeleteOtherStartStuff();
    }

    public void DeleteStartStuff()
    {
        Destroy(StartPanel.gameObject);
        timer = true;
        GamePanel.gameObject.SetActive(true);
    }

    public void DeleteOtherStartStuff()
    {
        for(int i = 0; i < TextsAndArrows.GetLength(0); i++)
        {
            Destroy(TextsAndArrows[i]);
        }

        timer = false;
        t = 0;
    }

}
