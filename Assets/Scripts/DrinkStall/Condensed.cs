using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condensed : Button
{
    void OnMouseUpAsButton()
    {
        base.OnMouseUpAsButton();
        Toolbox.Instance.Get<DrinksController>().condensed++;
    }
}
