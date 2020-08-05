using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarangGuniEntity : Entity
{
    public enum PART { ONE, TWO, THREE }
    public PART part;

    public KarangGuniController kgc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void InteractWith(Entity other)
    {
        base.InteractWith(other);
        switch (part)
        {
            case PART.ONE:
                kgc.SetState(KarangGuniController.STATE.END_KG);
            break;
        }
    }
}
