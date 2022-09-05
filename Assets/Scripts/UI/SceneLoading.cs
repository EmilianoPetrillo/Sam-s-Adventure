using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    /*[SerializeField]
    private Image _progressBar;
    // Start is called before the first frame update
    void Start()
    {  
        //Start async Operation
        StartCoroutine(LoadAsyncOperation());
        
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        while (gameLevel.progress < 1)
        {
            _progressBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(3.0f);
        AsyncOperation unloadLevel = SceneManager.UnloadSceneAsync("LoadingScreen");
        do
        {
            yield return null;
        }
        while (!unloadLevel.isDone);
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("GameControllers").SetActive(true);
        GameObject.Find("GameCanvas").SetActive(true);
        GameObject.Find("GameObjects").SetActive(true);

    }*/

}
