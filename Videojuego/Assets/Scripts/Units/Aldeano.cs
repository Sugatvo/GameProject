using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aldeano : UnitBehaviour
{
    private GameObject UIPanel;
    private UIAldeano UIController;


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
        BuildManager.instance.SetBuilding(0);
    }
    private void CreateBarracks()
    {
        BuildManager.instance.SetBuilding(1);
    }
    private void CreateOilStorage()
    {
        BuildManager.instance.SetBuilding(4);
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
        getAnimator().SetBool("running", isMoving());

    }

}
