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

        levelNameText.text = SceneManager.GetActiveScene().name;
        fadeScreen = debugCanvas.GetComponentInChildren<Image>();
        Toolbox.Instance.Sfx.StopAll();
        LeanTween.cancelAll();
        FadeIn();
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
        LeanTween.alpha(fadeScreen.GetComponent<RectTransform>(), 0.0f, 1.0f);
    }

    public void EndLevel()
    {
        var seq = LeanTween.sequence();
        seq.append(2.0f);
        seq.append(LeanTween.alpha(fadeScreen.GetComponent<RectTransform>(), 1.0f, 2.5f));
        seq.append(1.0f);
        seq.append(()=> {
            var nextScene = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(nextScene);
        });
    }
}
