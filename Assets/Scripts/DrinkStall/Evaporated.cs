using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaporated : Button
{
    void OnMouseUpAsButton()
    {
        base.OnMouseUpAsButton();
        Toolbox.Instance.Get<DrinksController>().evaporated++;
        Toolbox.Instance.Get<GameController>().Print("Added some evaporated milk to the stirring cup!");
    }
}
