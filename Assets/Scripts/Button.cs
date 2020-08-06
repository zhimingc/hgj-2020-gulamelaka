using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button : MonoBehaviour
{
    private PlayerController pc;
    private TextMeshPro tmp;

    private void Awake()
    {
        pc = FindObjectOfType<PlayerController>();
        tmp = GetComponentInChildren<TextMeshPro>();
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
    }

    void OnMouseUpAsButton()
    {
        Toolbox.Instance.Get<GameController>().Print(this.name + " is pressed");
    }


    void OnMouseExit()
    {
        Debug.Log("I have left something");
    }
}
