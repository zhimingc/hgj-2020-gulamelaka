using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEraser : MonoBehaviour
{
    public Vector2 powerRange;
    public float accuracyRange;
    public GameObject player;
    public CountryEraserController gameController;

    public void ShootAtPlayer()
    {
        Vector2 aimVec = (player.transform.position - transform.position).normalized;
        aimVec = Quaternion.AngleAxis(Random.Range(-accuracyRange, accuracyRange), Vector3.forward) * aimVec;
        float power = Random.Range(powerRange.x, powerRange.y);
        GetComponent<Rigidbody2D>().AddForce(aimVec * power);

        gameController.SetLastTurn(false);
        LeanTween.delayedCall(0.25f, ()=> {gameController.ProgressState();});
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Eraser")
        {
            gameController.EndGame();
        }
    }
}
