using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aldeano : UnitBehaviour
{
    private GameObject UIPanel;
    private UIAldeano UIController;
    private float nextHarvest;

    protected override void OnSelected()
    {
        UIPanel = UIManager.instance.ShowPanel(this, "Aldeano");
        UIController = UIPanel.GetComponent<UIAldeano>();
        UIController.CreateCentroUrbano.onClick.AddListener(CreateCentroUrbano);
        UIController.CreateBarracks.onClick.AddListener(CreateBarracks);
        UIController.CreateOilStorage.onClick.AddListener(CreateOilStorage);
        UIPanel.SetActive(true);
    }

    private void CreateCentroUrbano()
    {
        if (ResourceManager.player1_Hierro >= 500 && ResourceManager.player1_Madera >= 500)
        {
            ResourceManager.player1_Hierro = ResourceManager.player1_Hierro - 500;
            ResourceManager.player1_Madera = ResourceManager.player1_Madera - 500;
            BuildManager.instance.SetBuilding(0);
            ResourceManager.player1_Unidad_Max += 10;
        }
    }
    private void CreateBarracks()
    {
        
        if (ResourceManager.player1_Hierro >= 250 && ResourceManager.player1_Madera >= 300)
        {
            ResourceManager.player1_Hierro = ResourceManager.player1_Hierro - 250;
            ResourceManager.player1_Madera = ResourceManager.player1_Madera - 300;
            BuildManager.instance.SetBuilding(1);
        }
    }
    private void CreateOilStorage()
    {
        
        if (ResourceManager.player1_Hierro >= 50 && ResourceManager.player1_Madera >= 50)
        {
            ResourceManager.player1_Hierro = ResourceManager.player1_Hierro - 50;
            ResourceManager.player1_Madera = ResourceManager.player1_Madera - 50;
            ResourceManager.player1_Unidad_Max += 5;
            BuildManager.instance.SetBuilding(4);

        }
    }


    protected override void OnDeselected()
    {
        UIController.CreateCentroUrbano.onClick.RemoveListener(CreateCentroUrbano);
        UIController.CreateBarracks.onClick.RemoveListener(CreateBarracks);
        UIController.CreateOilStorage.onClick.RemoveListener(CreateOilStorage);
        UIManager.instance.ReleasePanel(this, "Aldeano");

        UIController = null;
        UIPanel = null;
    }

    protected override void OnAnimation()
    {
        getAnimator().SetBool("running", IsMoving());
        getAnimator().SetBool("mining", isMining());
        getAnimator().SetBool("farming", isFarming());
        getAnimator().SetBool("cutting", isCutting());
    }

    protected override void UpdateResources()
    {
        if (isMining() && Time.time > nextHarvest)
        {
            nextHarvest = Time.time + 3;
            ResourceManager.player1_Hierro += 5;
        }

        if (isFarming() && Time.time > nextHarvest)
        {
            nextHarvest = Time.time + 3;
            ResourceManager.player1_Comida += 5;
        }

        if (isCutting() && Time.time > nextHarvest)
        {
            nextHarvest = Time.time + 3;
            ResourceManager.player1_Madera += 5;
        }
    }



}
