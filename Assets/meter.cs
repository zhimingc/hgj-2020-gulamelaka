using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meter : MonoBehaviour
{
    private bool isCoroutineExecuting;
    // Start is called before the first frame update
    void Start()
    {
        isCoroutineExecuting = false;
        float originalRotation = 75.0f;
        var seq = LeanTween.sequence();
        seq.append(LeanTween.rotateZ(this.gameObject, originalRotation, 4.5f).setDelay(2.0f).setEaseInOutBounce());
        Toolbox.Instance.Sfx.PlaySound("drumroll_0", 0.25f);
        StartCoroutine(ExecuteAfterTime(4.5f));

    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        PlayLousyHumming();
    }

    void PlayLousyHumming()
    {
        Toolbox.Instance.Sfx.PlayLoop("lousy_humming_0", 0.25f);
    }
}
