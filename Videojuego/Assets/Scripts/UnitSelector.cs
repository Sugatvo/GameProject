using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSelector : MonoBehaviour
{
    public EventSystem eventSystem;
    public RectTransform selectorImage;
    Rect selectionRect;
    Vector2 startPos;
    Vector2 endPos;

    public bool selecting;


    // Start is called before the first frame update
    void Start()
    {
        DrawRect();
    }

    private bool ValidMouseClick()
    {
        return !BuildManager.instance.isBuilding && !eventSystem.IsPointerOverGameObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ValidMouseClick())
        {
            DeactivateAllUnits();
            startPos = Input.mousePosition;
            selecting = true;
        }

        if (Input.GetMouseButton(0) && selecting)
        {
            endPos = Input.mousePosition;
            DrawRect();

            // X axis
            if (Input.mousePosition.x < startPos.x)
            {
                selectionRect.xMin = Input.mousePosition.x;
                selectionRect.xMax = startPos.x;
            }
            else
            {
                selectionRect.xMin = startPos.x;
                selectionRect.xMax = Input.mousePosition.x;
            }

            // Y axis
            if (Input.mousePosition.y < startPos.y)
            {
                selectionRect.yMin = Input.mousePosition.y;
                selectionRect.yMax = startPos.y;
            }
            else
            {
                selectionRect.yMin = startPos.y;
                selectionRect.yMax = Input.mousePosition.y;
            }
        }

        if (Input.GetMouseButtonUp(0) && selecting)
        {
            CheckSelectedUnits();
            startPos = Vector2.zero;
            endPos = Vector2.zero;
            DrawRect();
            selecting = false;
        }
    }

    void DrawRect()
    {
        Vector2 boxStart = startPos;
        Vector2 center = (boxStart + endPos) / 2;

        selectorImage.position = center;

        float sizeX = Mathf.Abs(boxStart.x - endPos.x);
        float sizeY = Mathf.Abs(boxStart.y - endPos.y);

        selectorImage.sizeDelta = new Vector2(sizeX, sizeY);
    }

    void CheckSelectedUnits()
    {
        foreach (UnitBehaviour unit in SelectionManager.unitList)
        {
            if (selectionRect.Contains(Camera.main.WorldToScreenPoint(unit.transform.position)))
            {
                unit.SetSelected(true);
            }
        }
    }

    void DeactivateAllUnits()
    {
        foreach(UnitBehaviour unit in SelectionManager.unitList)
        {
            unit.SetSelected(false);
        }

    }
}
