using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelGrid : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        // create new colors array where the colors will be created.
        Color32[] colors = new Color32[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            if (i % 2 == 0)
            {
                colors[i] = Color.blue;
            }
        }

        // assign the array of colors to the Mesh.
        mesh.colors32 = colors;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
