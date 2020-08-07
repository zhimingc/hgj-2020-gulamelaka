using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Button
{
    void OnMouseUpAsButton()
    {
        base.OnMouseUpAsButton();
        Toolbox.Instance.Get<DrinksController>().water++;
        Toolbox.Instance.Get<GameController>().Print("Added some water to the stirring cup!");
    }
}
