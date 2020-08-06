using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Button
{
    void OnMouseUpAsButton()
    {
        base.OnMouseUpAsButton();
        Toolbox.Instance.Get<DrinksController>().water++;
    }
}
