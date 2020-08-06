using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stir : Button
{
    void OnMouseUpAsButton()
    {
        base.OnMouseUpAsButton();
        Toolbox.Instance.Get<DrinksController>().Stir();
    }
}
