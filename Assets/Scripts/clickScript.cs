using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickScript : MonoBehaviour
{
    public void StartNewGame()
    {
        GameObject.Find("LoadController").GetComponent<LoadController>().LoadNewRun();
    }
}
