using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BuildSelector : MonoBehaviour
{
    public LayerMask buildingsMask;
    public Image im;
    public EventSystem eventSystem;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildingsMask))
            {
                BuildingBehaviour building = hit.collider.gameObject.GetComponent<BuildingBehaviour>();
                if (building && building.built)
                {
                    if (building.selected)
                    {
                        building.SetSelected(false);
                    }
                    else
                    {
                        DeactivateAllBuildings();
                        building.SetSelected(true);
                    }
                }
            }

            else if (!eventSystem.IsPointerOverGameObject())
            {
                DeactivateAllBuildings();
            }

        }
    }

    void DeactivateAllBuildings()
    {
        foreach (BuildingBehaviour building in SelectionManager.buildList)
        {
                building.SetSelected(false);
        }

    }
}
