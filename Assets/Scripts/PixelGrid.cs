using System.Collections;
using UnityEngine;

public class PixelGrid : MonoBehaviour
{
    public int pixelsPaintedPerSecond = 60;
    
    [HideInInspector] public LayerMask targetMask;
    [HideInInspector] public Vector3 mainCameraPosition;

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
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            StartCoroutine(PaintChildPixels());
        }
    }

    private IEnumerator PaintChildPixels()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.GetComponent<Pixel>().PaintPixel();
            yield return new WaitForSeconds(1.0f / pixelsPaintedPerSecond);
        }
    }
}
