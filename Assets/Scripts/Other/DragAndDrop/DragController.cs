using UnityEngine;

public class DragController : MonoBehaviour
{
    public Draggable LastDragged => lastDragged;

    private bool isDragActive = false;

    private Vector2 screenPosition;

    private Vector3 worldPosition;

    private Draggable lastDragged;

    void Awake()
    {
        DragController[] controllers = FindObjectsOfType<DragController>();
        if (controllers.Length > 1)
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if (isDragActive)
        {
            if (Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                Drop();
                return;
            }
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if (Input.touchCount > 0)
        {
            screenPosition = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (isDragActive)
        {
            Drag();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider != null)
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
                if (draggable != null)
                {
                    lastDragged = draggable;
                    InitDrag();
                }
            }
        }
    }

    void InitDrag()
    {
        lastDragged.LastPosition = lastDragged.transform.position;
        UpdateDraStatus(true);
    }

    void Drag()
    {
        lastDragged.transform.position = new Vector2(worldPosition.x, worldPosition.y);
    }

    void Drop()
    {
        UpdateDraStatus(false);
    }

    void UpdateDraStatus(bool IsDragging)
    {
        isDragActive = lastDragged.IsDragging = IsDragging;
        lastDragged.gameObject.layer = IsDragging ? Layer.Dragging : Layer.Default;
    }
}


