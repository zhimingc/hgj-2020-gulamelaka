using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarangGuniEntity : Entity
{
    public enum PART { ONE, TWO, THREE }
    public PART part;

    public KarangGuniController kgc;
    public GameObject moneyObj;

    private Vector3 moneyPos = new Vector3(0.0f, -3.5f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAnimationTrigger(string triggerName)
    {
        GetComponent<Animator>().SetTrigger(triggerName);
    }

    override public void InteractWith(Entity other)
    {
        base.InteractWith(other);
        switch (part)
        {
            case PART.ONE:
                kgc.SetState(KarangGuniController.STATE.END_KG);
                moneyObj.SetActive(true);

                // give money
                float moneyTime = 1.5f;
                moneyObj.transform.position = transform.position;
                moneyObj.transform.localScale = Vector3.zero;
                LeanTween.move(moneyObj, moneyPos, moneyTime);
                LeanTween.scale(moneyObj, new Vector3(1.5f, 1.5f, 1.0f), moneyTime);
            break;
        }
    }
}
