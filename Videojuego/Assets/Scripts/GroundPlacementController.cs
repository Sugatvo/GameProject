using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject placeableObjectPrefab;

    [SerializeField]
    private KeyCode newObjectHotKey = KeyCode.A;

    private GameObject currentPlaceableObject;


    // Update is called once per frame
    private void Update()
    {
        HandleNewObjectHotKey();

        if(currentPlaceableObject != null)
        {
            MoveCurrentPlaceableObjectToMouse();
            ReleaseIfClicked();
        }
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableObject = null;
        }
    }

    private void MoveCurrentPlaceableObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            currentPlaceableObject.transform.position = hit.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }

    private void HandleNewObjectHotKey()
    {
        if (Input.GetKeyDown(newObjectHotKey))
        {
            if(currentPlaceableObject == null)
            {
                currentPlaceableObject = Instantiate(placeableObjectPrefab);
            }
            else
            {
                Destroy(currentPlaceableObject);
            }
        }
    }
}
