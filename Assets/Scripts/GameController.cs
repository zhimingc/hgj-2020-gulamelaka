using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text debugText;
    public Text levelNameText;
    public string debugLog = "";

    private Image fadeScreen;
    public int nextScene;

    public string[] sceneNames = { "karang guni #1", "karang guni #2", "country eraser #1", "country eraser #2"};

    private void Awake() {
        Init();
        SceneManager.sceneLoaded += LevelLoaded;
    }

    void Init()
    {
        var debugCanvas = Instantiate(Resources.Load("Prefabs/DebugCanvas")) as GameObject;
        debugCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
        foreach (var textComp in debugCanvas.GetComponentsInChildren<Text>())
        {
            if (textComp.name == "debug_text") debugText = textComp;
            if (textComp.name == "level_name_text") levelNameText = textComp;
        }

        fadeScreen = debugCanvas.GetComponentInChildren<Image>();
        Toolbox.Instance.Sfx.StopAll();
        LeanTween.cancelAll();

        if (SceneManager.GetActiveScene().name == "load-screen")
        {
            levelNameText.text = sceneNames[nextScene-1];
            FadeInLevelName();
        }
        else
        {
            FadeIn();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name != "main-menu")
            {
                SceneManager.LoadScene("main-menu");
            }
            else
            {
                Application.Quit();
            }
        }
    }

    public void Print(string text)
    {
        debugLog = debugLog.Insert(0, "- " + text + "\n");
        debugText.text = debugLog;
    }

    void LevelLoaded(Scene scene, LoadSceneMode mode) 
    {
        Init();
    }

    void FadeIn()
    {
        var seq = LeanTween.sequence();
        seq.append(LeanTween.alpha(fadeScreen.GetComponent<RectTransform>(), 0.0f, 1.0f));
    }

    void FadeInLevelName()
    {
        var seq = LeanTween.sequence();
        seq.append(LeanTween.alphaText(levelNameText.GetComponent<RectTransform>(), 1.0f, 1.5f));
        seq.append(1.5f);
        seq.append(LeanTween.alphaText(levelNameText.GetComponent<RectTransform>(), 0.0f, 1.5f));
        seq.append(0.5f);
        seq.append(()=> {
            SceneManager.LoadScene(nextScene);
        });
    }

    public void EndLevel()
    {
        nextScene = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        EndLevel("load-screen");
    }

    public void EndLevel(string nextLevel)
    {
        var seq = LeanTween.sequence();
        seq.append(1.0f);   // time befoe fading starts
        seq.append(LeanTween.alpha(fadeScreen.GetComponent<RectTransform>(), 1.0f, 2.5f));
        seq.append(1.0f);   // time before scene is loaded
        seq.append(()=> {
            SceneManager.LoadScene(nextLevel);
        });
    }
}
