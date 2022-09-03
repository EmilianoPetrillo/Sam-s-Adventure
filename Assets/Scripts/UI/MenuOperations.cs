using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOperations : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("LoadingScreen");
    }
}
