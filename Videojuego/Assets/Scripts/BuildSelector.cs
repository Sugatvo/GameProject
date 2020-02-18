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
                Debug.Log("Hit build");
                if (hit.collider.gameObject.GetComponent<Build>().GetSelector())
                {
                    hit.collider.gameObject.GetComponent<Build>().SetSelector(false);
                }
                else
                {
                    hit.collider.gameObject.GetComponent<Build>().SetSelector(true);
                }

            }

            else if (!eventSystem.IsPointerOverGameObject())
            { 
                foreach (Build b in SelectionManager.buildList)
                {
                    b.SetSelector(false);
                }
            }

        }
    }
}
