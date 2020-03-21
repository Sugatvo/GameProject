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
        GameObject unit = Instantiate(UnitTop, transform.TransformPoint(SpawnPosition), transform.rotation);
    }
    private void CreateUnitBot()
    {
        GameObject unit = Instantiate(UnitBot, transform.TransformPoint(SpawnPosition), transform.rotation);

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
