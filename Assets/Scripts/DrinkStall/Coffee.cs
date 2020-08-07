using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : Button
{
    void OnMouseUpAsButton()
    {
        base.OnMouseUpAsButton();
        Toolbox.Instance.Get<DrinksController>().coffee++;
        Toolbox.Instance.Get<GameController>().Print("Added some coffee to the stirring cup!");
    }
}
