using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountryEraserEntity_1 : Entity
{
    public enum SEQUENCE { START, PICK_POWER, FLIPPED }
    public SEQUENCE sequence;
    public GameObject powerBar;
    public GameObject pickBar;

    public float correctRange = 0.3f;
    public float pickSpeed = 10.0f;
    
    private float originalScale = 0.0f;
    private float currentPick = 0.0f;
    private int pickDir = 1;

    private void Start()
    {
        originalScale = transform.localScale.x;
    }

    private void Update()
    {
        switch (sequence)
        {
            case SEQUENCE.START:
            break;
            case SEQUENCE.PICK_POWER:
                UpdatePickBar();
            break;
            case SEQUENCE.FLIPPED:
            break;
        }    
    }

    void ShootEraser()
    {
        switch (GetPickAmount())
        {
            case 0:
                var seq = LeanTween.sequence();
                seq.append(LeanTween.scaleY(gameObject, 0.0f, 0.1f));
                seq.append(()=> {
                    GetComponentInChildren<TextMeshPro>().text = "flipped country eraser";
                });
                seq.append(LeanTween.scaleY(gameObject, originalScale, 0.1f));
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 500.0f));
                GetComponent<Rigidbody2D>().AddTorque(2.0f);
            break;
            case 1:
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 1750.0f));
                GetComponent<Rigidbody2D>().AddTorque(150.0f);
            break;
            case -1:
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 300.0f));
                GetComponent<Rigidbody2D>().AddTorque(0.5f);
            break;
        }

        sequence = SEQUENCE.FLIPPED;
        powerBar.SetActive(false);
    }

    void UpdatePickBar()
    {
        currentPick += pickDir * Time.deltaTime;
        if (pickDir == 1 && currentPick > 1.0f)
        {
            pickDir = -1;
            currentPick = 1.0f;
        }
        else if (currentPick < 0.0f)
        {
            pickDir = 1;
            currentPick = 0.0f;
        }

        var barPos = pickBar.GetComponent<RectTransform>().localPosition;
        barPos.y = -125.0f + currentPick * 250.0f;
        pickBar.GetComponent<RectTransform>().localPosition = barPos;
    }

    int GetPickAmount()
    {
        Vector2 range = new Vector2(0.5f - correctRange / 2.0f, 0.5f + correctRange / 2.0f);
        if (currentPick < range.x) return -1;
        if (currentPick > range.y) return 1;
        return 0;
    }

    void OnMouseDown()
    {
        switch (sequence)
        {
            case SEQUENCE.START:
                sequence = SEQUENCE.PICK_POWER;
                powerBar.SetActive(true);
            break;
            case SEQUENCE.PICK_POWER:
                ShootEraser();
            break;
            case SEQUENCE.FLIPPED:
            break;
        }
    }
}
