using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController Instance;

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

    private int coins;
    private int coinsToUpgradeATK = 100;
    private int keyATK = 0;
    private int coinsToUpgradeHP = 100;
    private int keyHP = 0;
    private int coinsToUpgradeCRITRATE = 100;
    private int keyCRITRATE = 0;
    private int coinsToUpgradeCRITDAMAGE = 100;
    private int keyCRITDAMAGE = 0;
    public Text actualCoins;
    public Text stageAndLevel;

    public GameObject bossButton;
    public bool level9Completed;

    private void Update()
    {
        actualCoins.text = coins.ToString();
        stageAndLevel.text = GameController.Instance.Stage.ToString() + " - " + GameController.Instance.Level.ToString();
        if (bossButton.activeSelf == false && GameController.Instance.Level == 9 && level9Completed)
            bossButton.SetActive(true);
        else if (bossButton.activeSelf == true && GameController.Instance.Level != 9)
            bossButton.SetActive(false);
    }

    public void CoinsUp(float gainedCoins)
    {
        coins += (int)gainedCoins;
    }
    public void BossButton()
    {
        GameController.Instance.LevelUp();
        GameController.Instance.ForcedLevelUp();
    }
    #region STATS UPGRADE BUTTONS
    public void AtkUpgrade()
    {
        if (coins >= coinsToUpgradeATK)
        {
            coins -= coinsToUpgradeATK;
            keyATK++;
            coinsToUpgradeATK += 100 * keyATK;
            Player.Instance.ATKUpgrade();
        }
    }

    public void HpUpgrade()
    {
        if (coins >= coinsToUpgradeHP)
        {
            coins -= coinsToUpgradeHP;
            keyHP++;
            coinsToUpgradeHP += 100 * keyHP;
            Player.Instance.HPUpgrade();
        }
    }

    public void CritRateUpgrade()
    {
        if (coins >= coinsToUpgradeCRITRATE)
        {
            coins -= coinsToUpgradeCRITRATE;
            keyCRITRATE++;
            coinsToUpgradeCRITRATE += 100 * keyCRITRATE;
            Player.Instance.CRITRATEUpgrade();
        }
    }

    public void CritDamageUpgrade()
    {
        if (coins >= coinsToUpgradeCRITDAMAGE)
        {
            coins -= coinsToUpgradeCRITDAMAGE;
            keyCRITDAMAGE++;
            coinsToUpgradeCRITDAMAGE += 100 * keyCRITDAMAGE;
            Player.Instance.CRITDAMAGEUpgrade();
        }
    }
    #endregion

}
