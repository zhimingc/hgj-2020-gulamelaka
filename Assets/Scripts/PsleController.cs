using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PsleController : MonoBehaviour
{
    public GameObject anchor;
    private void Awake() {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.StartsWith("psle")) Init();
    }
    
    void Init()
    {

        anchor = GameObject.Find("Anchor");

        IntroAnimation();
    
    }

    void IntroAnimation()
    {
        var seq = LeanTween.sequence();
        seq.append(LeanTween.scale(anchor, new Vector3(2.0f, 2.0f, 2.0f), 0.5f).setEase(LeanTweenType.easeInOutBack));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Mark()
    {
        Debug.Log("Herere");
        int marks = 0;
        Transform answerBoxes = GameObject.Find("Answer Boxes").transform;
        int childCount = answerBoxes.childCount;
        for (int i=0; i<childCount; i++)
        {
            AnswerBox ab = answerBoxes.GetChild(i).GetComponent<AnswerBox>();
            if(string.Equals(ab.finalAnswer, ab.intendedAnswer))
            {
                marks++;
            }
        }
        
        Debug.Log("Marks: " + marks + "/" + childCount);
    }
}
