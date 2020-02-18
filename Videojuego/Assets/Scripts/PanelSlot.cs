using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSlot : MonoBehaviour
{
    //public Image icon;
    public Image panel;

    private bool State;

    void Start()
    {
        HidePanel();
    }

    public void ShowPanel()
    {
        //icon.enabled = true;
        //panel.enabled = true;
        panel.gameObject.SetActive(true);
        State = true;
    }

    public void HidePanel()
    {
        //icon.enabled = false;
        //panel.enabled = false;
        panel.gameObject.SetActive(false);
        State = false;
    }

    public bool GetState()
    {
        return State;
    }
        
}
