using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneEntity : Entity
{
    public bool isOn = false;
    public KarangGuniController kgc;
    public GameObject moneyObj;

    private void OnMouseDown() 
    {
        if (!isOn)
        {
            isOn = true;
            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponentInChildren<TextMeshPro>().enabled = false;
        }
    }

    override public void InteractWith(Entity other)
    {
        if (!isOn) 
        {
            other.ReturnToOrigin();
            return;
        }
        other.InteractOn(this);
        Destroy(other.gameObject);
        kgc.SetState(KarangGuniController.STATE.END_CAROUSELL);
        moneyObj.transform.position = transform.position;
        LeanTween.move(moneyObj, transform.position + Vector3.up * 2.5f, 1.0f);
        LeanTween.alpha(moneyObj, 0.0f, 1.5f);
        LeanTween.delayedCall(1.5f, () => {
            moneyObj.SetActive(false);
        });
    }
}
