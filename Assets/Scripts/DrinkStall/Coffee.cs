using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : Button
{
    void OnMouseUpAsButton()
    {
        base.OnMouseUpAsButton();
        Toolbox.Instance.Get<DrinksController>().coffee++;
    }
}
