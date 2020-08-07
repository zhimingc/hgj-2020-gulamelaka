using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartText : MonoBehaviour
{
    public Text starText;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.delayedCall(10.0f, ()=> {
            Toolbox.Instance.Gc.FirstLevel();
        });
    }
}
