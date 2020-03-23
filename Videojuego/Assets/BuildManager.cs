using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public bool isBuilding;

    public BuildingObject[] Buildings;
    private BuildingObject building;
    private GameObject preview;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        ProcessInput();
    }
    
    private void GetInput()
    {
        int input = -1;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            input = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            input = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            input = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            input = 4;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            input = 5;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            input = 6;
        if (Input.GetKeyDown(KeyCode.Alpha7))
            input = 7;
        if (Input.GetKeyDown(KeyCode.Alpha8))
            input = 8;
        if (Input.GetKeyDown(KeyCode.Alpha9))
            input = 9;
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Escape))
            input = 0;

        if (input != -1)
        {
            if (input == 0)
            {
                if (preview)
                {
                    Destroy(preview);
                    preview = null;
                    building = null;
                }
                isBuilding = false;
            }
            else
                SetBuilding(--input);
        }
    }

    private void ProcessInput()
    {
        if (!preview)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 mousePos = hit.point;
            preview.transform.position = mousePos + building.Offset;
        }

        if (Input.GetMouseButton(0))
        {
            BuildingBehaviour buildingBehaviour = preview.GetComponent<BuildingBehaviour>();
            if (buildingBehaviour.validPosition)
            {
                buildingBehaviour.Place();
                if (Input.GetKey(KeyCode.LeftShift))
                    preview = CreatePreview(building);
                else
                {
                    preview = null;
                    building = null;
                    isBuilding = false;
                }
            }
        }
    }

    public void SetBuilding(int id)
    {
        isBuilding = true;

        if (preview)
        {
            Destroy(preview);
            preview = null;
            building = null;
        }

        if (id >= 0 && id < Buildings.Length)
        {
            building = Buildings[id];
            preview = CreatePreview(building);
        }
    }

    private GameObject CreatePreview(BuildingObject building)
    {
        GameObject go = Instantiate(building.Prefab);
        go.transform.SetParent(transform);
        return go;
    }
}
