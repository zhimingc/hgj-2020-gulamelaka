using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PsleController : MonoBehaviour
{
    public GameObject anchor;
    public int marks = 0;

    private void Awake() {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.StartsWith("psle")) Init();
    }
    
    void Init()
    {
        Toolbox.Instance.Sfx.PlayLoop("classroom_0", 0.1f);

        anchor = GameObject.Find("Anchor");

        Transform answerBoxes = GameObject.Find("Answer Boxes").transform;
        int childCount = answerBoxes.childCount;
        for (int i=0; i<childCount; i++)
        {
            answerBoxes.GetChild(i).Find("Correct").gameObject.SetActive(false);
            answerBoxes.GetChild(i).Find("Wrong").gameObject.SetActive(false);
        }
        IntroAnimation();
    
    }

    void IntroAnimation()
    {
        anchor.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        var seq = LeanTween.sequence();
        seq.append(LeanTween.scale(anchor, new Vector3(2.0f, 2.0f, 2.0f), 1.0f).setEase(LeanTweenType.easeInOutBack));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Mark()
    {
        marks = 0;
        Transform answerBoxes = GameObject.Find("Answer Boxes").transform;
        int childCount = answerBoxes.childCount;
        var seq = LeanTween.sequence();
        for (int i=0; i<childCount; i++)
        {
            AnswerBox ab = answerBoxes.GetChild(i).GetComponent<AnswerBox>();
            GameObject markGraphic = null;
            if(string.Equals(ab.finalAnswer, ab.intendedAnswer))
            {
                marks++;
                markGraphic = answerBoxes.GetChild(i).Find("Correct").gameObject;
                
            }
            else
            {
                markGraphic = answerBoxes.GetChild(i).Find("Wrong").gameObject;
            }
            
            markGraphic.SetActive(true);
            Vector3 originalScale = markGraphic.transform.localScale;
            //LeanTween.alpha(markGraphic, 0.0f, 0.01f);
            //seq.insert(LeanTween.alpha(markGraphic, 1.0f, 2.0f));
            seq.append(LeanTween.scale(markGraphic, originalScale * 1.1f, 0.1f).setDelay(1.0f).setEaseInOutBounce().setLoopPingPong(2));
            Toolbox.Instance.Sfx.PlaySound("scribble_0", 0.25f, 0.9f, 1.1f);
        }
        
        Debug.Log("Marks: " + marks + "/" + childCount);
    }
}
