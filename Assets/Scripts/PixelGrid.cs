using System.Collections;
using UnityEngine;

public class PixelGrid : MonoBehaviour
{
    public int pixelsPaintedPerSecond = 60;
    public Vector3 dummyCameraPosition;
    public LayerMask targetMask;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            StartCoroutine(PaintChildPixels());
        }
        
        else if (Input.GetKeyDown(KeyCode.R))
        {
            StopAllCoroutines();
            ResetPixels();
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

    private void ResetPixels()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.GetComponent<Pixel>().ResetColor();
        }
    }
}
