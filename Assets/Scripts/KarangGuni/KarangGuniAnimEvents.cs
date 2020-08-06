using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarangGuniAnimEvents : MonoBehaviour
{
    public KarangGuniController kgc;

    public void KarangGuniWalkEnd()
    {
        kgc.SetState(KarangGuniController.STATE.END_NOTHING);
    }
}
