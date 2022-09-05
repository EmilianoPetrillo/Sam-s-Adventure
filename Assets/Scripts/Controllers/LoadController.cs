using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine(LoadGame());   
    }

    IEnumerator LoadGame()
    {

        Fader.Instance.FadeOut(1.5f);
        AsyncOperation loadLogoScreen = SceneManager.LoadSceneAsync("InitLogoScreen", LoadSceneMode.Additive);
        //let things catch up a bit under the hood, let loading spinner at least slightly appear...
        do
        {
            yield return null;
        }
        while (!loadLogoScreen.isDone);
        yield return new WaitForSeconds(2.5f);
        Fader.Instance.FadeIn(1.5f);
        yield return new WaitForSeconds(1.5f);
        Fader.Instance.FadeOut(1.5f);
        AsyncOperation menuScreen = SceneManager.LoadSceneAsync("MenuScreen", LoadSceneMode.Additive);
        //let things catch up a bit under the hood, let loading spinner at least slightly appear...
        do
        {
            yield return null;
        }
        while (!menuScreen.isDone);
        yield return new WaitForSeconds(1.5f);
        AsyncOperation unloadingLogoScene = SceneManager.UnloadSceneAsync("InitLogoScreen");
        do
        {
            yield return null;
        }
        while (!unloadingLogoScene.isDone);
        GameObject.Find("BackgroundSound").GetComponent<AudioSource>().Play();
    }

    private IEnumerator LoadNewRunOperations()
    {
        GameObject.Find("MenuCamera").SetActive(false);
        GameObject.Find("MenuCanvas").SetActive(false);
        GameObject.Find("MenuEventSystem").SetActive(false);
        AsyncOperation loadLogoScreen = SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
        do
        {
            yield return null;
        }
        while (!loadLogoScreen.isDone);
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        Image _progressBar = GameObject.Find("FillBarLoadingScreen").GetComponent<Image>();
        float fill = 0.2f;
        do
        {
            
            _progressBar.fillAmount = fill;
            yield return new WaitForSeconds(0.2f);
            fill += 0.2f;
        }
        while (!gameLevel.isDone);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        UIController.Instance.StartStuff();
        yield return new WaitForSeconds(1.5f);
        AsyncOperation unloadLevel = SceneManager.UnloadSceneAsync("LoadingScreen");
        do
        {
            yield return null;
        }
        while (!unloadLevel.isDone);
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("GameControllers").GetComponent<ScreenController>().ShowScreen(true);
        GameController.Instance.StartStuff();
    }

    public void LoadNewRun()
    {
        StartCoroutine(LoadNewRunOperations());
    }
}
