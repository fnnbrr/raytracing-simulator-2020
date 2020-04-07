using UnityEngine;

public class PixelGrid : MonoBehaviour
{
    public LayerMask targetMask;
    public Vector3 mainCameraPosition;
    public bool paintThisFrame;

    private void Start()
    {
        targetMask = LayerMask.GetMask("target");

        if (Camera.main == null)
        {
            throw new UnityException("No main camera found");
        }
        mainCameraPosition = Camera.main.transform.position;
    }

    private void Update()
    {
        paintThisFrame = Input.GetMouseButtonDown(0);
    }
}
