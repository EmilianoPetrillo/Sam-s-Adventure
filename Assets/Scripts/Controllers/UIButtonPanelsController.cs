using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonPanelsController : MonoBehaviour
{
    public GameObject SceneGameObjects;
    public GameObject[] Panels;
    private int prevIndex = -1;
    public void manageUIButtonPanelOpen(int index)
    {
        if (SceneGameObjects.activeSelf)
        {
            GameController.Instance.DestroyEnemy();
            SceneGameObjects.SetActive(false);
        }
        for (int i = 0; i < Panels.Length; i++)
        {
            if (i == index)
            {
                Panels[i].SetActive(true);
            }
            else
            {
                Panels[i].SetActive(false);
            }
        }
    }

    public void manageUIButtonPanelClose()
    {
        if (!SceneGameObjects.activeSelf)
        {
            GameController.Instance.DestroyEnemy();
            GameController.Instance.SpawnEnemy();
            SceneGameObjects.SetActive(true);
        }
        for (int i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(false);
        }
    }

    public void togglePanel(int index)
    {
        if (index != prevIndex)
        {
            manageUIButtonPanelOpen(index);
            prevIndex = index;
        }
        else
        {
            manageUIButtonPanelClose();
            prevIndex = -1;
        }
    }
}
