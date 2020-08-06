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
    public Sprite sadKG;

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
                kgObj.GetComponent<SpriteRenderer>().sprite = sadKG;
                Toolbox.Instance.Gc.EndLevel();
            break;
            case STATE.END_KG:
                kgAnim.enabled = false;
                kgObj.SetAnimationTrigger("idle");
                Toolbox.Instance.Gc.EndLevel();
            break;
            case STATE.END_NOTHING:
                var seq = LeanTween.sequence();
                seq.append(0.5f);
                seq.append(()=> {
                    kgAnim.SetTrigger("walkBack");
                    kgObj.SetAnimationTrigger("idle");
                    kgObj.GetComponent<SpriteRenderer>().sprite = sadKG;}
                );
                seq.append(1.0f);
                seq.append(()=> {Toolbox.Instance.Gc.EndLevel();});
            break;
        }

        state = newState;
        if ((int)state >= 2)
        {
        }
    }
}
