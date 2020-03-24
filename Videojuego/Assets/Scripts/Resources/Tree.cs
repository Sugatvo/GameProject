using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : ResourceBehaviour
{
    public static int Recolectado = 0;

    private GameObject UIPanel;


    protected override void OnSelected()
    {
        UIPanel = UI_Info.instance.ShowPanel(this, 2);
        UIPanel.SetActive(true);
    }


    protected override void OnDeselected()
    {
        UI_Info.instance.ReleasePanel(this, 2);
        UIPanel = null;
    }
}
