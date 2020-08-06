using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagOfClothes : Entity
{
    public enum PART { ONE, TWO, THREE }
    public KarangGuniController kgc;

    override public void InteractOn(Entity other)
    {
        if (other.name == "recycling_bin")
        {
            kgc.SetState(KarangGuniController.STATE.END_BIN);
        }
    }
}
