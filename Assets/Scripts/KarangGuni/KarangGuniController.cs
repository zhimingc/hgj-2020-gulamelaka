using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KarangGuniController : MonoBehaviour
{
    public enum STATE { START, KG_MOVING, END_BIN, END_KG, END_NOTHING, END_CAROUSELL }
    public STATE state;

    public GameObject kgParent;
    public KarangGuniEntity kgObj;
    public float startDelay;

    private Animator kgAnim;

    // Start is called before the first frame update
    void Start()
    {
        kgAnim = kgParent.GetComponent<Animator>();
        Toolbox.Instance.Sfx.PlaySound("karang_guni_1", 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case STATE.START:
                state = STATE.KG_MOVING;
                LeanTween.delayedCall(gameObject, startDelay, () => {
                    kgAnim.SetTrigger("walk");
                    kgObj.SetAnimationTrigger("walk");
                });
            break;
        }
    }

    public void SetState(STATE newState)
    {
        switch (newState)
        {
            case STATE.END_BIN:
            case STATE.END_CAROUSELL:
                kgObj.GetComponentInChildren<TextMeshPro>().enabled = true;
            break;
            case STATE.END_KG:
                kgAnim.enabled = false;
                kgObj.SetAnimationTrigger("idle");
            break;
            case STATE.END_NOTHING:
                //if (state == STATE.END_BIN || state == STATE.END_CAROUSELL) break;
                LeanTween.delayedCall(gameObject, 1.0f, ()=> {
                    kgAnim.SetTrigger("walkBack");
                    kgObj.SetAnimationTrigger("idle");
                    kgObj.GetComponentInChildren<TextMeshPro>().enabled = true;
                });
            break;
        }

        state = newState;
        if ((int)state >= 2)
        {
            Toolbox.Instance.Gc.EndLevel();
        }
    }
}
