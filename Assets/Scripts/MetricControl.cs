using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetricControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.delayedCall(15.0f, ()=> {
            Toolbox.Instance.Gc.EndLevel("main-menu");
        });
    }

}
