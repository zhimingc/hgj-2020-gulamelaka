using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaporated : Button
{
    void OnMouseUpAsButton()
    {
        base.OnMouseUpAsButton();
        Toolbox.Instance.Get<DrinksController>().evaporated++;
    }
}
