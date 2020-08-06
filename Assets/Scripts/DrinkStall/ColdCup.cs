using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdCup : Button
{
    void OnMouseUpAsButton()
    {
        base.OnMouseUpAsButton();
        Toolbox.Instance.Get<DrinksController>().ColdCup();
    }
}
