using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button : MonoBehaviour
{
    private PlayerController pc;
    private TextMeshPro tmp;

    private Vector3 originalScale;

    private void Awake()
    {
        pc = FindObjectOfType<PlayerController>();
        tmp = GetComponentInChildren<TextMeshPro>();
        originalScale = transform.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        tmp.GetComponent<MeshRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0.0f;
    }

    void OnMouseEnter()
    {
        Debug.Log("I am over something");
        transform.localScale = originalScale * 1.1f;
        this.GetComponentInChildren<TextMeshPro>().enabled = true;
    }

    void OnMouseDown()
    {
        transform.localScale = originalScale * 0.9f;
    }

    protected void OnMouseUpAsButton()
    {
        Toolbox.Instance.Get<GameController>().Print(this.name + " is pressed");
    }

    void OnMouseExit()
    {;
        Debug.Log("I have left something");
        transform.localScale = originalScale;
        this.GetComponentInChildren<TextMeshPro>().enabled = false;
    }
}
