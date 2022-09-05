using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance;

    public GameObject MenuPanel;
    public GameObject OptionsPanel;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void ShowMainMenu(bool _show)
    {
        MenuPanel.SetActive(_show);

        // check if existing game, show LoadGame button depending on this.
    }

    public void ShowOptionsMenu(bool _show)
    {
        OptionsPanel.SetActive(_show);
    }

    public void NewGameButtonPressed()
    {

    }

    public void LoadGameButtonPressed()
    {

    }

    public void OptionsButtonPressed()
    {
        ShowMainMenu(false);
        ShowOptionsMenu(true);
    }

    public void BackButtonPressed()
    {
        ShowMainMenu(true);
        ShowOptionsMenu(false);
    }
}