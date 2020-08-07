using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerBox : Entity
{
    public PsleController pc;
    public string finalAnswer;
    public GameObject finalAnswerObject;
    public string intendedAnswer;

    override public void Start()
    {
        
    }
    
    void Wake()
    {
        finalAnswer = "";
    }

    override public void Update()
    {
        if(!string.Equals(finalAnswer, ""))
        {
            if (!finalAnswerObject.GetComponent<BoxCollider2D>().bounds.Intersects(this.GetComponent<BoxCollider2D>().bounds))
            {
                finalAnswer = "";
            }
        }
        base.Update();
    }
    public void setFinalAnswer(string answer, GameObject answerObject)
    {
        finalAnswer = answer;
        finalAnswerObject = answerObject;
    }
}
