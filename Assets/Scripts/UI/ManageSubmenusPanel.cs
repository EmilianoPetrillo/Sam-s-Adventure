using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageSubmenusPanel : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject textWeaponMenu;
    public GameObject textStatsMenu;
    public GameObject otherPanel;
    public void TogglePanel()
    {
        if(currentPanel != null && otherPanel != null)
        {
            bool isActive = currentPanel.activeSelf;
            if(otherPanel.activeSelf)
            {
                otherPanel.SetActive(false);
            }
            textWeaponMenu.SetActive(isActive);
            textStatsMenu.SetActive(isActive);
            currentPanel.SetActive(!isActive);
        }
    }
}
