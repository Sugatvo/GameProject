using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GameObject selector;
    public HideAndShow panel;
    private bool State;

    void Start()
    {
        State = false;
        SetSelector(false);
        SelectionManager.buildList.Add(this);
    }

    public void SetSelector(bool statement)
    {
        selector.SetActive(statement);
        State = statement;
    }

    public bool GetSelector()
    {
        return State;
    }

    // Update is called once per frame
    void Update()
    {
        if (State == true)
        {
            if (!panel.GetState())
            {
                panel.ShowPanel();
            }
       
        }
        else
        {
            panel.HidePanel();
        }

    }



}
