using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEraser : MonoBehaviour
{
    public Vector2 powerRange;
    public float accuracyRange;
    public GameObject player;
    public CountryEraserController gameController;

    public bool checkEndTurn;

    public void ShootAtPlayer()
    {
        Vector2 aimVec = (player.transform.position - transform.position).normalized;
        aimVec = Quaternion.AngleAxis(Random.Range(-accuracyRange, accuracyRange), Vector3.forward) * aimVec;
        float power = Random.Range(powerRange.x, powerRange.y);
        GetComponent<Rigidbody2D>().AddForce(aimVec * power);

        gameController.SetLastTurn(false);
        checkEndTurn = true;
    }

    private void Update()
    {
        if (checkEndTurn && GetComponent<Rigidbody2D>().velocity.magnitude < 0.1f)
        {
            checkEndTurn = false;
            gameController.ProgressState();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Eraser" && 
            other.GetComponent<Rigidbody2D>().velocity.magnitude < 0.5f && 
            GetComponent<Rigidbody2D>().velocity.magnitude < 0.5f)
        {
            gameController.EndGame();
        }
    }
}
