using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button : MonoBehaviour
{
    private PlayerController pc;
    private TextMeshPro tmp;

    private Vector3 originalScale;
    public bool isTextReamin;

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
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // mousePos.z = 0.0f;
    }

    virtual public void OnMouseEnter()
    {
        transform.localScale = originalScale * 1.1f;
        this.GetComponentInChildren<TextMeshPro>().enabled = true;
    }

    void OnMouseDown()
    {
        transform.localScale = originalScale * 0.9f;
    }

    protected void OnMouseUpAsButton()
    {
        //Toolbox.Instance.Get<GameController>().Print(this.name + " is pressed");
    }

    virtual public void OnMouseExit()
    {
        transform.localScale = originalScale;
        if (!isTextReamin) this.GetComponentInChildren<TextMeshPro>().enabled = false;
    }
}
