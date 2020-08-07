using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugar : Button
{
    void OnMouseUpAsButton()
    {
        base.OnMouseUpAsButton();
        Toolbox.Instance.Get<DrinksController>().sugar++;
        Toolbox.Instance.Get<GameController>().Print("Added some sugar to the stirring cup!");
    }
}
