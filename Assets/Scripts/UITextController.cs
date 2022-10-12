using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UITextController : MonoBehaviour
{

    public static UITextController Instance;

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

    public GameObject[] TextsAndArrows;

    public GameObject GamePanel;
    public GameObject StartPanel;
    public GameObject[] Backgrounds;
    public GameObject CurrentBackground;
    public Transform SpawnPosition;
    public GameObject Portal;
    public GameObject IntroWorldTextPanel;
    private int i = 0;
    private int j = 4;
    private List<string> worldTitles;
    private List<string> worldSubtitles;
    
    
    private void Start()
    {
        worldTitles = new List<string>();
        worldSubtitles = new List<string>();
        AddTitlesAndSubtitles();
    }

    private void AddTitlesAndSubtitles()
    {
        worldTitles.Add("first universe");
        worldSubtitles.Add("medieval fantasy");
        worldTitles.Add("second universe");
        worldSubtitles.Add("warriors and demons");
        worldTitles.Add("third universe");
        worldSubtitles.Add("the elemental");
        worldTitles.Add("fourth universe");
        worldSubtitles.Add("the space");
    }

    public void DeleteStartStuff()
    {
        Destroy(StartPanel.gameObject);
        GamePanel.gameObject.SetActive(true);
    }

    public void ChangeGamePanel()
    {
        if(CurrentBackground != null)
        {
            Destroy(CurrentBackground);
            i++;
            UIController.Instance.BoxPieces[i - 1].SetActive(true);
        }
        CurrentBackground = Instantiate(Backgrounds[i], Backgrounds[i].transform.position, Quaternion.identity);
    }


    public void NextPhase()
    {
        if(j < TextsAndArrows.GetLength(0))
            TextsAndArrows[j].SetActive(false);
        if(j >= TextsAndArrows.GetLength(0) - 1)
        {
            StartCoroutine(changePanelAndPortal());
        }
        else
        {
            j++;
            TextsAndArrows[j].SetActive(true);
        }
    }

    private IEnumerator changePanelAndPortal()
    {
        GameController.Instance.DestroyEnemy();
        if (GameController.Instance.Stage == 10 && GameController.Instance.Level == 1) {
            UIController.Instance.Before4WorldPanel.SetActive(true);
        }
        while(UIController.Instance.Before4WorldPanel.activeSelf == true)
        {
            yield return null;
        }

        //Integrate this changes for portal spawning
        GameObject portal = Instantiate(Portal, new Vector3(-3.5f, -1.75f, 0.0f), Quaternion.identity);
        yield return new WaitForSeconds(1);
        DitzeGames.Effects.CameraEffects.ShakeOnce(3);
        yield return new WaitForSeconds(4);
        Destroy(portal);
        ChangeGamePanel();
        StartCoroutine(showIntroText());
        GameController.Instance.SpawnEnemy();
    }

    private IEnumerator showIntroText()
    {
        Text[] textobjs = IntroWorldTextPanel.GetComponentsInChildren<Text>();
        textobjs[0].text = worldTitles[i];
        textobjs[1].text = worldSubtitles[i];
        IntroWorldTextPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        IntroWorldTextPanel.SetActive(false);
    }

}
