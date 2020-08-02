using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PLAYER_STATE
{
    IDLE,
    SELECT,
    DRAGGING
}

public class PlayerController : MonoBehaviour
{
    public PLAYER_STATE state;
    public Entity selectedEntity;
    public List<Entity> hoveredEntity;
    public bool isNewState;

    private Vector3 mousePos;

    void Awake()
    {
        selectedEntity = null;
        isNewState = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        DetectRaycast();
        StartSM();
        UpdateSM();

        //DEBUG
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void DetectRaycast()
    {
        hoveredEntity.Clear();

        // Check for overlaps with the mouse
        var colliders = Physics2D.OverlapPointAll(new Vector2(mousePos.x, mousePos.y));

        // If it hits something...
        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                var entity = collider.GetComponent<Entity>();
                if (entity != null)
                {
                    hoveredEntity.Add(entity);
                }
            }
        }
    }

    void UpdateSM()
    {
        switch (state)
        {
            case PLAYER_STATE.IDLE:
            break;
            case PLAYER_STATE.SELECT:
            break;
            case PLAYER_STATE.DRAGGING:
                mousePos.z = 0.0f;
                selectedEntity.transform.position = mousePos;
                if (Input.GetMouseButtonUp(0))
                {
                    DisengageEntity();
                }
            break;
        }
    }

    void StartSM()
    {
        if (isNewState)
        {
            isNewState = false;
            switch (state)
            {
                case PLAYER_STATE.IDLE:
                break;
                case PLAYER_STATE.SELECT:
                break;
                case PLAYER_STATE.DRAGGING:
                break;
            }            
        }
    }

    void SetState(PLAYER_STATE newState)
    {
        state = newState;
        isNewState = true;
    } 

    public void InteractWithEntity(Entity dragged, PLAYER_STATE newState)
    {
        selectedEntity = dragged;
        SetState(newState);
    }

    public void DisengageEntity()
    {
        foreach (var hovered in hoveredEntity)
        {
            if (selectedEntity == null) break;
            if (hovered == selectedEntity) continue;
            hovered.InteractWith(selectedEntity);
        }
        SetState(PLAYER_STATE.IDLE);
        selectedEntity = null;
    }
}
