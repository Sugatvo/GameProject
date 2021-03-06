﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ResourceBehaviour : MonoBehaviour
{
    protected bool started { get; private set; }
    private GameObject selector;

    public bool selected { get; private set; }

    private void Start()
    {
        SelectionManager.resourceList.Add(this);
        selector = transform.Find("Selector").gameObject;
        if (selector == null)
            Debug.LogError("RESOURCE HAS NOT SELECTED!");
        selector.SetActive(false);
        OnStart();
        started = true;
    }

    protected virtual void OnStart() { }

    public void SetSelected(bool state)
    {
        if (state)
        {
            if (!selected)
            {
                selected = true;
                selector.SetActive(true);
                OnSelected();
            }
        }
        else
        {
            if (selected)
            {
                selected = false;
                selector.SetActive(false);
                OnDeselected();
            }
        }
    }

    protected virtual void OnSelected() { }
    protected virtual void OnDeselected() { }
}
