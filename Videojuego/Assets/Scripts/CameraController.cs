using UnityEngine;


public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 5f;
    public Vector3 panLimit;
    public float scrollSpeed = 20f;

    public int min = 15;
    public int max = 40;

    private Camera cameraMain;

    private void Awake()
    {
        cameraMain = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            panSpeed = 2;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            panSpeed /= 2;

       Vector3 pos = transform.position;

        pos += transform.forward * Input.GetAxis("Vertical") * panSpeed;
        pos += transform.right * Input.GetAxis("Horizontal") * panSpeed;

        pos.x = Mathf.Clamp(pos.x, -25, panLimit.x);
        pos.y = 60;
        pos.z = Mathf.Clamp(pos.z, -25, panLimit.z);

        cameraMain.orthographicSize = Mathf.Clamp(cameraMain.orthographicSize - (1 * Input.mouseScrollDelta.y * scrollSpeed), min, max);

        transform.localPosition = pos;
    }
}
