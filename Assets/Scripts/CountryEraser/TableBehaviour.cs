using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableBehaviour : MonoBehaviour
{
    public CountryEraserController cec;

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.name == "Eraser") cec.DropEraser(true);
        if (other.name == "OppEraser") cec.DropEraser(false);
    }
}
