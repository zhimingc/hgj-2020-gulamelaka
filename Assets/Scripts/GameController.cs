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
}
