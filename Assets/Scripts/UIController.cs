using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    private int coins;
    public Text actualCoins;
    public Text stageAndLevel;
    GameController GameController;

    private void Start()
    {
        GameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        actualCoins.text = coins.ToString();
        stageAndLevel.text = GameController.Stage.ToString() + " - " + GameController.Level.ToString();
    }

    public void CoinsUp(int gainedCoins)
    {
        coins += gainedCoins;
    }

}
