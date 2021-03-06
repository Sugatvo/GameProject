﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResourceSelector : MonoBehaviour
{
    public LayerMask resourceMask;
    public EventSystem eventSystem;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, resourceMask))
            {
                ResourceBehaviour resource = hit.collider.gameObject.GetComponent<ResourceBehaviour>();
                if (resource)
                {
                    if (resource.selected)
                    {
                        resource.SetSelected(false);
                    }
                    else
                    {
                        DeactivateAllResources();
                        resource.SetSelected(true);
                    }
                }

            }
            else if (!eventSystem.IsPointerOverGameObject())
            {
                DeactivateAllResources();
            }

        }
    }


    void DeactivateAllResources()
    {
        foreach (ResourceBehaviour r in SelectionManager.resourceList)
        {
            r.SetSelected(false);
        }

    }
}