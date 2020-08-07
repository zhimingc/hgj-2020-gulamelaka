using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryEraserController : MonoBehaviour
{
    public enum GAME_STATE { PLAYER_TURN, AI_TURN, PLAYER_WIN, PLAYER_LOSE, PLAYER_OFF, AI_OFF }
    public GAME_STATE state;
    public CountryEraserEntity_2 playerEraser;
    public AIEraser aiEraser;
    public Text endText;
    public GameObject endObj;

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

        endObj.gameObject.SetActive(true);
        if (state == GAME_STATE.PLAYER_WIN)
        {
            endText.text = "you won their eraser!";
            playerEraser.GetComponent<SpriteRenderer>().sortingOrder = 2;
            aiEraser.GetComponent<SpriteRenderer>().sortingOrder = 1;
            Toolbox.Instance.finalScore = Toolbox.Instance.finalScore + 1.0f;    
        }
        else
        {
            endText.text = "you lost your eraser";
            playerEraser.GetComponent<SpriteRenderer>().sortingOrder = 1;
            aiEraser.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }

        Toolbox.Instance.Gc.EndLevel();
    }

    public void DropEraser(bool isPlayer)
    {
        LeanTween.cancel(gameObject);
        state = isPlayer ? GAME_STATE.PLAYER_OFF : GAME_STATE.AI_OFF;
        endObj.gameObject.SetActive(true);
        if (!isPlayer)
        {
            endText.text = "you won their eraser!";    
            LeanTween.scale(aiEraser.gameObject, Vector3.zero, 1.0f);
            Toolbox.Instance.finalScore = Toolbox.Instance.finalScore + 1.0f;
        }
        else
        {
            endText.text = "you lost your eraser";
            playerEraser.transform.localScale = Vector3.one * playerEraser.originalScale;         
            LeanTween.scale(playerEraser.gameObject, Vector3.zero, 1.0f);   
        }

        Toolbox.Instance.Gc.EndLevel();
    }

}
