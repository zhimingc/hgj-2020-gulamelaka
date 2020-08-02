using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text debugText;
    public string debugLog = "";

    private void Awake() {
        var debugCanvas = Instantiate(Resources.Load("Prefabs/DebugCanvas")) as GameObject;
        debugText = debugCanvas.GetComponentInChildren<Text>();
    }

    public void Print(string text)
    {
        debugLog = debugLog.Insert(0, "- " + text + "\n");
        debugText.text = debugLog;
    }
}
