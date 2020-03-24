using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : BuildingBehaviour
{
    public Vector3 SpawnPosition;
    public GameObject UnitTop;
    public GameObject UnitBot;

    private GameObject UIPanel;
    private UIBarracks UIController;

    protected override void OnSelected()
    {
        UIPanel = UIManager.instance.ShowPanel(this, 1);
        UIController = UIPanel.GetComponent<UIBarracks>();
        UIController.CreateTop.onClick.AddListener(CreateUnitTop);
        UIController.CreateBot.onClick.AddListener(CreateUnitBot);
        UIPanel.SetActive(true);
    }

    private void CreateUnitTop()
    {
        if (ResourceManager.player1_Comida >= 100 && ResourceManager.player1_Hierro >= 100)
        {
            if (ResourceManager.player1_Unidad_Actual < ResourceManager.player1_Unidad_Max)
            {
                ResourceManager.player1_Comida -= 100;
                ResourceManager.player1_Hierro -= 100;
                GameObject unit = Instantiate(UnitTop, transform.TransformPoint(SpawnPosition), transform.rotation);

            }
        }
    }
    private void CreateUnitBot()
    {
        if (ResourceManager.player1_Comida >= 150 && ResourceManager.player1_Hierro >= 150 && ResourceManager.player1_Madera >= 150)
        {
            if (ResourceManager.player1_Unidad_Actual < ResourceManager.player1_Unidad_Max)
            {
                ResourceManager.player1_Comida -= 150;
                ResourceManager.player1_Hierro -= 150;
                ResourceManager.player1_Madera -= 150;
                GameObject unit = Instantiate(UnitBot, transform.TransformPoint(SpawnPosition), transform.rotation);    
            }
        }
    }


    protected override void OnDeselected()
    {
        UIController.CreateTop.onClick.RemoveListener(CreateUnitTop);
        UIController.CreateBot.onClick.RemoveListener(CreateUnitBot);
        UIManager.instance.ReleasePanel(this, 1);

        UIController = null;
        UIPanel = null;
    }
}
