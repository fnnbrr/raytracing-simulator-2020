﻿using System.Collections;
using UnityEngine;

public class PixelGrid : MonoBehaviour
{
    public int pixelsPaintedPerSecond = 60;
    public Vector3 dummyCameraPosition;
    public LayerMask targetMask;

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
