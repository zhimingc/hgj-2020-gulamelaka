using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotCup : Button
{
    void OnMouseUpAsButton()
    {
        base.OnMouseUpAsButton();
        Toolbox.Instance.Get<DrinksController>().HotCup();
    }
}
