
using UnityEngine;
using UnityEngine.UI;

public class CreateUnit : MonoBehaviour
{
    [SerializeField]
    private GameObject placeableObjectPrefab;

    //Make sure to attach these Buttons in the Inspector
    public Button Unit;

    private GameObject currentPlaceableObject;

    void Start()
    {
        Button btn = Unit.GetComponent<Button>();
        //Calls the TaskOnClick method when you click the Button
        btn.onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        if (currentPlaceableObject != null)
        {
            MoveCurrentPlaceableObjectToMouse();
            ReleaseIfClicked();
        }
    }

    void TaskOnClick()
    {
        if (currentPlaceableObject == null)
        {
            currentPlaceableObject = Instantiate(placeableObjectPrefab);
        }
        else
        {
            Destroy(currentPlaceableObject);
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

        if (Physics.Raycast(ray, out hit))
        {
            currentPlaceableObject.transform.position = hit.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }

    

}
