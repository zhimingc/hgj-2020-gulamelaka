using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountryEraserEntity_2 : Entity
{
    public enum STATE { IDLE, AIMING, TOSSED }
    public STATE eraserState = STATE.IDLE;
    public GameObject aimArrow;
    public CountryEraserController gameController;
    public List<float> powerLevels;
    public float originalScale;

    private float arrowOffset = 1.0f;
    private float arrowPowerOffset = 0.5f;
    private float[] powerLevelDist = {0.0f, 1.0f, 2.0f};
    private Vector3 shootVec;
    private int powerLevel;

    private void Start()
    {
        originalScale = transform.localScale.x;        
    }

    private void Update()
    {
        switch (eraserState)
        {
            case STATE.IDLE:
            break;
            case STATE.AIMING:
                UpdateAiming();
                if (Input.GetMouseButtonUp(0))
                {
                    TossEraser();
                    aimArrow.SetActive(false);
                    eraserState = STATE.TOSSED;
                }
            break;
            case STATE.TOSSED:
                // can't do anything until game controller says it's your turn
                if (gameController.state == CountryEraserController.GAME_STATE.PLAYER_TURN && GetComponent<Rigidbody2D>().velocity.magnitude < 0.1f)
                {
                    gameController.ProgressState();
                }
            break;
        }
    }

    void UpdateAiming()
    {
        float shootDist = (transform.position - GetMouseWorldPos()).magnitude;
        for (int i = powerLevelDist.Length - 1; i >= 0; --i)
        {
            if (shootDist > powerLevelDist[i])
            {
                powerLevel = i;
                aimArrow.transform.localScale = Vector3.one * (powerLevel + 1);
                break;
            }
        }

        shootVec = (transform.position - GetMouseWorldPos()).normalized;
        aimArrow.transform.position = transform.position + shootVec * (arrowOffset + arrowPowerOffset * powerLevel);
        aimArrow.transform.up = transform.position - aimArrow.transform.position;
    }

    void TossEraser()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(shootVec.x, shootVec.y) * powerLevels[powerLevel]);
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(-25.0f, 25.0f));

        var seq = LeanTween.sequence();
        seq.append(LeanTween.scaleY(gameObject, 0.0f, 0.1f));
        seq.append(LeanTween.scaleY(gameObject, originalScale, 0.1f));
    }

    void OnMouseDown()
    {
        switch (eraserState)
        {
            case STATE.IDLE:
                eraserState = STATE.AIMING;
                aimArrow.SetActive(true);
            break;
            case STATE.AIMING:
            break;
            case STATE.TOSSED:
            break;
        }
    }

    Vector3 GetMouseWorldPos()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0.0f;
        return pos;
    }

    public void SetIdle()
    {
        eraserState = STATE.IDLE;
    }
}
