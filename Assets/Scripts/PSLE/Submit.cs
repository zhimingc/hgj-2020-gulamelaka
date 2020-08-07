using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Submit : Button
{
    private PlayerController pc;
    private TextMeshPro tmp;

    private Vector3 originalScale;

    private PsleController psle;

    private void Awake()
    {
        pc = FindObjectOfType<PlayerController>();
        tmp = GetComponentInChildren<TextMeshPro>();
        originalScale = transform.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // mousePos.z = 0.0f;
    }

    override public void OnMouseEnter()
    {
        transform.localScale = originalScale * 1.1f;
    }

    void OnMouseDown()
    {
        transform.localScale = originalScale * 0.9f;
    }

    protected void OnMouseUpAsButton()
    {
        Debug.Log("on mouse up");
        Toolbox.Instance.Get<PsleController>().Mark();
        
        base.OnMouseUpAsButton();
    }

    override public void OnMouseExit()
    {
        transform.localScale = originalScale;
    }
}
