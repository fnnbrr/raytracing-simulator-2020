using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelGrid : MonoBehaviour
{
    private Mesh mesh;
    private LayerMask targetMask;
    private Vector3 mainCameraPosition;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        targetMask = LayerMask.GetMask("target");

        if (Camera.main == null)
        {
            throw new UnityException("No main camera found");
        }
        mainCameraPosition = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Color32[] newColors32 = new Color32[mesh.vertexCount];
            
            for (int vertex = 0; vertex < mesh.vertexCount; vertex++)
            {
                if (Physics.Linecast(mainCameraPosition, mesh.vertices[vertex], targetMask))
                {
                    newColors32[vertex] = new Color32(0, 0, 255, 0);
                }
            }

            mesh.colors32 = newColors32;
        }
    }
}
