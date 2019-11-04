using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    public RectTransform selectorImage;
    Rect selectionRect;
    Vector2 startPos;
    Vector2 endPos;

    // Start is called before the first frame update
    void Start()
    {
        DrawRect();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DeactivateAllUnits();
            startPos = Input.mousePosition;
            
        }

        if (Input.GetMouseButton(0))
        {
            endPos = Input.mousePosition;
            DrawRect();

            // X axis
            if(Input.mousePosition.x < startPos.x)
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

        if (Input.GetMouseButtonUp(0))
        {
            CheckSelectedUnits();
            startPos = Vector2.zero;
            endPos = Vector2.zero;
            DrawRect();
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
        foreach (Unit unit in SelectionManager.unitList)
        {
            if (selectionRect.Contains(Camera.main.WorldToScreenPoint(unit.transform.position)))
            {
                unit.SetSelector(true);
            }
        }
        
    }

    void DeactivateAllUnits()
    {
        foreach(Unit unit in SelectionManager.unitList)
        {
            unit.SetSelector(false);
        }
    }
}
