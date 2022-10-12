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

    #region STATS AND COINS

    //TODO: Fix coins later for build
    private int coins = 0;
    public int Coins => coins;
    private int coinsToUpgradeATK = 100;
    public int keyATK = 0;
    private int coinsToUpgradeHP = 100;
    public int keyHP = 0;
    private int coinsToUpgradeCRITRATE = 100;
    public int keyCRITRATE = 0;
    private int coinsToUpgradeCRITDAMAGE = 100;
    public int keyCRITDAMAGE = 0;

    #endregion

    public Text actualCoins;
    public Text stageAndLevel;

    public GameObject bossButton;
    public GameObject BossButtonStuff;
    public bool level9Completed;
    public bool bossButtonStuffShown = false;

    public GameObject GamePanel;
    public GameObject DeathPanel;
    public GameObject StatsPanel;
    public GameObject EndGamePanel;
    public GameObject Before4WorldPanel;
    public GameObject DevourerKilledPanel;
    public Text HPUpCost;
    public Text ATKUpCost;
    public Text CRITRATEUpCost;
    public Text CRITDMGUpCost;
    public Text CurrentATK;
    public Text CurrentHP;
    public Text CurrentCRITRATE;
    public Text CurrentCRITDMG;
    public List<GameObject> BoxPieces;
    public Image ActiveGunImage;

    private void Start()
    {
    }
    private void Update()
    {
        actualCoins.text = coins.ToString();
        stageAndLevel.text = GameController.Instance.Stage.ToString() + " - " + GameController.Instance.Level.ToString();
        if (bossButton.activeSelf == false && GameController.Instance.Level == GameController.Instance.maxLevel - 1 && level9Completed)
        {
            bossButton.SetActive(true);
            if (!bossButtonStuffShown)
            {
                BossButtonStuff.SetActive(true);
                bossButtonStuffShown = true;
            }
        }
        else if (bossButton.activeSelf == true && GameController.Instance.Level != GameController.Instance.maxLevel - 1)
        {
            bossButton.SetActive(false);
            if (bossButtonStuffShown)
            {
                BossButtonStuff.SetActive(false);
            }

        }

        HPUpCost.text = coinsToUpgradeHP.ToString();
        ATKUpCost.text = coinsToUpgradeATK.ToString();
        CRITRATEUpCost.text = coinsToUpgradeCRITRATE.ToString();
        CRITDMGUpCost.text = coinsToUpgradeCRITDAMAGE.ToString();
        if (Player.Instance != null)
        {
            CurrentATK.text = Player.Instance.PlayerSO.ATK.ToString();
            CurrentHP.text = Player.Instance.PlayerSO.MAXHP.ToString();
            CurrentCRITRATE.text = Player.Instance.PlayerSO.CRITRATE.ToString();
            CurrentCRITDMG.text = Player.Instance.PlayerSO.CRITDAMAGE.ToString();
        }
    }

    public void CoinsUp(float gainedCoins)
    {
        coins += (int)gainedCoins;
    }

    public void CoinsDown(float spendCoins)
    {
        coins -= (int)spendCoins;
    }

    public void BossButton()
    {
        GameController.Instance.ForcedLevelUp();
    }

    #region PANELS STUFF

    public void StartStuff()
    {
        //GameController.Instance.StartStuff();
        UITextController.Instance.DeleteStartStuff();
    }

    public void DeathPanelOn()
    {
        GamePanel.SetActive(false);
        DeathPanel.SetActive(true);
    }

    public void DeathPanelOff()
    {
        GamePanel.SetActive(true);
        DeathPanel.SetActive(false);
        GameController.Instance.ForcedLevelDown();
    }

    public void StatsPanelOff()
    {
        StatsPanel.SetActive(false);
    }

    public void EndGamePanelStuff()
    {
        GamePanel.SetActive(false);
        EndGamePanel.SetActive(true);
    }

    #endregion

    #region STATS UPGRADE BUTTONS

    private bool tutorial = false;

    public void AtkUpgrade()
    {
        if (coins >= coinsToUpgradeATK)
        {
            coins -= coinsToUpgradeATK;
            keyATK++;
            coinsToUpgradeATK += 25 * keyATK;
            Player.Instance.ATKUpgrade();
            //Diminuito costo del 15% rispetto alla precedente build
            coinsToUpgradeATK = coinsToUpgradeATK * 85 / 100;
            if (tutorial)
            {
                UITextController.Instance.NextPhase();
                tutorial = false;
            }
        }
    }

    public void HpUpgrade()
    {
        if (coins >= coinsToUpgradeHP)
        {
            coins -= coinsToUpgradeHP;
            keyHP++;
            coinsToUpgradeHP += 25 * keyHP;
            //Diminuito costo del 15% rispetto alla precedente build
            coinsToUpgradeHP = coinsToUpgradeHP * 85 / 100;
            Player.Instance.HPUpgrade();
            if (tutorial)
            {
                UITextController.Instance.NextPhase();
                tutorial = false;
            }
        }
    }

    public void CritRateUpgrade()
    {
        if (coins >= coinsToUpgradeCRITRATE)
        {
            coins -= coinsToUpgradeCRITRATE;
            keyCRITRATE++;
            coinsToUpgradeCRITRATE += 25 * keyCRITRATE;
            //Diminuito costo del 15% rispetto alla precedente build
            coinsToUpgradeCRITRATE = coinsToUpgradeCRITRATE * 85 / 100;
            Player.Instance.CRITRATEUpgrade();
            if (tutorial)
            {
                UITextController.Instance.NextPhase();
                tutorial = false;
            }
        }
    }

    public void CritDamageUpgrade()
    {
        if (coins >= coinsToUpgradeCRITDAMAGE)
        {
            coins -= coinsToUpgradeCRITDAMAGE;
            keyCRITDAMAGE++;
            coinsToUpgradeCRITDAMAGE += 25 * keyCRITDAMAGE;
            //Diminuito costo del 15% rispetto alla precedente build
            coinsToUpgradeCRITDAMAGE = coinsToUpgradeCRITDAMAGE * 85 / 100;
            Player.Instance.CRITDAMAGEUpgrade();
            if (tutorial)
            {
                UITextController.Instance.NextPhase();
                tutorial = false;
            }
        }
    }

    public void FinishedCutsceneBefore4World()
    {
        Before4WorldPanel.SetActive(false);
        PlayerInventoryUI.Instance.AddWeaponToInventory(ItemController.Instance.Items.GetWeapon(WeaponItem.eWeaponType.LastBreath));

    }

    public void FinishedCutsceneDevourerKilled()
    {
        DevourerKilledPanel.SetActive(false);
        GameController.Instance.EndGame();
    }
    #endregion

}
