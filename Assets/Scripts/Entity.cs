using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public enum ENTITY_STATE
{
    IDLE,
    SELECT,
    DRAG
}

public enum INTERACT_TYPE
{
    NOTHING,
    DESTROY
}

public class Entity : MonoBehaviour
{
    public INTERACT_TYPE type;
    public ENTITY_STATE state = ENTITY_STATE.IDLE;
    public bool isDraggable = true;
    private PlayerController pc;
    private TextMeshPro tmp;

    private void Awake() {
        pc = FindObjectOfType<PlayerController>();
        tmp = GetComponentInChildren<TextMeshPro>(); 
    }

    private void Start()
    {
        tmp.GetComponent<MeshRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
    }

    private void Update()
    {
        switch (state)
        {
            case ENTITY_STATE.DRAG:
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0.0f;
                transform.position = mousePos;
            break;
        }
    }

    void OnMouseDown()
    {
        if (isDraggable)
        {
            pc.InteractWithEntity(this, PLAYER_STATE.DRAGGING);
            state = ENTITY_STATE.DRAG;
        }
    }

    virtual public void Disengage()
    {
        state = ENTITY_STATE.IDLE;
    }

    virtual public void InteractWith(Entity other)
    {
        switch (type)
        {
            case INTERACT_TYPE.NOTHING:
                Toolbox.Instance.Get<GameController>().Print(this.name + " interacting with " + other.name);
            break;
            case INTERACT_TYPE.DESTROY:
                Toolbox.Instance.Get<GameController>().Print("Disposing of " + other.name + " with " + this.name);
                Destroy(other.gameObject);
            break;
        }
    }
}
