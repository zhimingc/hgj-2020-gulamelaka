using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer : Entity
{
    public PsleController pc;

    override public void Start()
    {
        
    }
    override public void InteractOn(Entity other)
    {
        Debug.Log("Interacting with " + other.name);
        if (other.name.StartsWith("Answer Box"))
        {
            this.transform.position = other.transform.position;
            AnswerBox ab = other.GetComponent<AnswerBox>();
            ab.setFinalAnswer(this.name, this.gameObject);
        }
    }
}
