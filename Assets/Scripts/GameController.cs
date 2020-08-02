using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text debugText;
    public string debugLog = "";

    private void Awake() {
        Init();
        SceneManager.sceneLoaded += LevelLoaded;
    }

    void Init()
    {
        var debugCanvas = Instantiate(Resources.Load("Prefabs/DebugCanvas")) as GameObject;
        debugCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
        debugText = debugCanvas.GetComponentInChildren<Text>();
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
