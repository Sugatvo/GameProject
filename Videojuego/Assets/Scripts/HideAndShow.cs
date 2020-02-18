using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndShow : MonoBehaviour
{
    public GameObject mPanel;
    private bool State;

    void Start()
    {
        HidePanel();
    }

    public void ShowPanel()
    {
        mPanel.SetActive(true);
        State = true;
    }

    public void HidePanel()
    {
        mPanel.SetActive(false);
        State = false;
    }

    public bool GetState()
    {
        return State;
    }

}
