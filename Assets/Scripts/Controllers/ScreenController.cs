using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [SerializeField]
    private GameObject GameCanvas;
    [SerializeField]
    private GameObject MainGame;
    public void ShowScreen(bool active)
    {
        GameCanvas.SetActive(active);
        MainGame.SetActive(active);
    }
}
