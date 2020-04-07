using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 10;
    public float rotateSpeed = 5;
    public float scrollSpeed = 30;
    
    private Vector3 _previousMousePosition;

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            HandlePanning();
        }
        
        else if (Input.GetMouseButton(1))
        {
            HandleRotating();
        }

        if (!Mathf.Approximately(Input.GetAxis("Mouse ScrollWheel"), 0f))
        {
            HandleScrolling();
        }
    }

    private void HandlePanning()
    {
        // LMB has just been clicked
        if (Input.GetMouseButtonDown(0))
        {
            _previousMousePosition = Input.mousePosition;
        }

        // LMB is being held down
        else
        {
            Vector3 panDirection = Camera.main.ScreenToViewportPoint(Input.mousePosition - _previousMousePosition);
            panDirection.y *= -1;  // Inverts Y-axis movement to feel more like "dragging"
            transform.Translate(panSpeed * panDirection, Space.World);

            _previousMousePosition = Input.mousePosition;
        }
    }

    private void HandleRotating()
    {
        float rotateVertical = rotateSpeed * Input.GetAxis("Mouse X");
        float rotateHorizontal = -rotateSpeed * Input.GetAxis("Mouse Y");

        Vector3 newRotation = transform.rotation.eulerAngles +
                              new Vector3(rotateHorizontal, rotateVertical, 0);
        
        transform.rotation = Quaternion.Euler(newRotation);
    }

    private void HandleScrolling()
    {
        float scrollAmount = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.fieldOfView -= scrollSpeed * scrollAmount;
    }
}
