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

        if (ResourceManager.player1_Comida >= 50 && ResourceManager.player1_Hierro >= 50)
        {
            if(ResourceManager.player1_Unidad_Actual < ResourceManager.player1_Unidad_Max)
            {
                ResourceManager.player1_Comida -= 50;
                ResourceManager.player1_Hierro -= 50;
                GameObject unit = Instantiate(Unit, transform.TransformPoint(SpawnPosition), transform.rotation);
                
            }
        }
            
    }


    protected override void OnDeselected()
    {
        UIController.CreateUnitButton.onClick.RemoveListener(CreateUnit);
        UIManager.instance.ReleasePanel(this, 0);

        UIController = null;
        UIPanel = null;
    }
}
