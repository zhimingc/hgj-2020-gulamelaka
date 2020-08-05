using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarangGuniController : MonoBehaviour
{
    public enum STATE { START, KG_MOVING, END_BIN, END_KG, END_NOTHING }
    public STATE state;

    public GameObject kgObj;
    public float startDelay;

    private Animator kgAnim;

    // Start is called before the first frame update
    void Start()
    {
        kgAnim = kgObj.GetComponent<Animator>();
        Toolbox.Instance.Sfx.PlaySound("karang_guni_1");
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
                });
            break;
        }
    }

    public void SetState(STATE newState)
    {
        state = newState;

        switch (state)
        {
            case STATE.END_KG:
                kgAnim.enabled = false;
            break;
        }
    }
}
