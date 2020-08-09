using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tea : Button
{
    void OnMouseUpAsButton()
    {
        base.OnMouseUpAsButton();
        Toolbox.Instance.Get<DrinksController>().tea++;
        Toolbox.Instance.Get<GameController>().Print("Added some tea to the stirring cup!");
    }
}
