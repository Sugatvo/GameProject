using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentroUrbano : BuildingBehaviour
{
    public Vector3 SpawnPosition;
    public GameObject Unit;

    private GameObject UIPanel;
    private UICentroUrbano UIController;

    protected override void OnSelected()
    {
        UIPanel = UIManager.instance.ShowPanel(this, 0);
        UIController = UIPanel.GetComponent<UICentroUrbano>();
        UIController.CreateUnitButton.onClick.AddListener(CreateUnit);
        UIPanel.SetActive(true);
    }

    private void CreateUnit()
    {
        GameObject unit = Instantiate(Unit, transform.TransformPoint(SpawnPosition), transform.rotation);
    }


    protected override void OnDeselected()
    {
        UIController.CreateUnitButton.onClick.RemoveListener(CreateUnit);
        UIManager.instance.ReleasePanel(this, 0);

        UIController = null;
        UIPanel = null;
    }

    protected override void OnCanBuild() 
    {
        int madera_Actual = ResourceManager.player1_Madera;
    }
}
