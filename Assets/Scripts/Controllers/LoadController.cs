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
        GameObject parentMenuObject = GameObject.Find("MenuItemsToSwitch");
        foreach (Transform g in parentMenuObject.transform)
            g.gameObject.SetActive(false);
        AsyncOperation loadLogoScreen = SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
        do
        {
            yield return null;
        }
        while (!loadLogoScreen.isDone);
        AsyncOperation cutscenesScreen = SceneManager.LoadSceneAsync("InitialCutscenes", LoadSceneMode.Additive);
        Image _progressBar = GameObject.Find("FillBarLoadingScreen").GetComponent<Image>();
        float fill = 0.2f;
        do
        {
            
            _progressBar.fillAmount = fill;
            yield return new WaitForSeconds(0.2f);
            fill += 0.2f;
        }
        while (fill != 1.0f);
        yield return new WaitForSeconds(1f);
        GameObject.Find("NarrativePanel").GetComponent<CutscenesTypedText>().StartShowSlides();
        AsyncOperation unloadLevel = SceneManager.UnloadSceneAsync("LoadingScreen");
        do
        {
            yield return null;
        }
        while (!unloadLevel.isDone);
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator LoadGameOperations()
    {
        Fader.Instance.FadeIn(1f);
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        do
        {
            yield return null;
        }
        while (!gameLevel.isDone);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        yield return new WaitForSeconds(1f);
        UIController.Instance.StartStuff();
        Fader.Instance.FadeOut(1f);
        AsyncOperation unloadingCutscenesScene = SceneManager.UnloadSceneAsync("InitialCutscenes");
        do
        {
            yield return null;
        }
        while (!unloadingCutscenesScene.isDone);
        yield return new WaitForSeconds(1f);
        GameObject.Find("GameControllers").GetComponent<ScreenController>().ShowScreen(true);
        
    }
    private IEnumerator BackToMenuOperations()
    {
        Fader.Instance.FadeIn(1f);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuScreen"));
        GameObject parentMenuObject = GameObject.Find("MenuItemsToSwitch");
        parentMenuObject.transform.GetChild(2).gameObject.SetActive(true);
        AsyncOperation unloadingCutscenesScene = SceneManager.UnloadSceneAsync("Game");
        do
        {
            yield return null;
        }
        while (!unloadingCutscenesScene.isDone);
        
        yield return new WaitForSeconds(1f);
        foreach (Transform g in parentMenuObject.transform)
            g.gameObject.SetActive(true);
        Fader.Instance.FadeOut(1f);
        
    }
    public void LoadNewRun()
    {
        StartCoroutine(LoadNewRunOperations());
    }

    public void LoadGameScene()
    {
        StartCoroutine(LoadGameOperations());
    }

    public void BackToMainMenu()
    {
        StartCoroutine(BackToMenuOperations());
    }
}
