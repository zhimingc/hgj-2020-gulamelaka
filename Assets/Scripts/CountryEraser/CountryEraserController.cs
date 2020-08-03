using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryEraserController : MonoBehaviour
{
    public enum GAME_STATE { PLAYER_TURN, AI_TURN, PLAYER_WIN, PLAYER_LOSE }
    public GAME_STATE state;
    public CountryEraserEntity_2 playerEraser;
    public AIEraser aiEraser;
    public Text endText;

    private GAME_STATE lastTurn;

    private void Update()
    {
        // debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ProgressState();
        }
    }

    public void ProgressState()
    {
        switch (state)
        {
            case GAME_STATE.PLAYER_TURN:
                state = GAME_STATE.AI_TURN;
                SetLastTurn(true);
                LeanTween.delayedCall(gameObject, 0.5f, ()=> {aiEraser.ShootAtPlayer();});
            break;
            case GAME_STATE.AI_TURN:
                state = GAME_STATE.PLAYER_TURN;
                playerEraser.SetIdle();
            break;
        }
    }

    public void SetLastTurn(bool isPlayer)
    {
        lastTurn = isPlayer ? GAME_STATE.PLAYER_TURN : GAME_STATE.AI_TURN;
    }

    public void EndGame()
    {
        LeanTween.cancel(gameObject);
        state = lastTurn == GAME_STATE.PLAYER_TURN ? GAME_STATE.PLAYER_WIN : GAME_STATE.PLAYER_LOSE;

        endText.enabled = true;
        if (state == GAME_STATE.PLAYER_WIN)
        {
            endText.text = "you won their eraser";
        }
        else
        {
            endText.text = "you lost your eraser";
        }
    }

}
