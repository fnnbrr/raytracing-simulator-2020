using UnityEngine;

public class Pixel : MonoBehaviour
{
    public float maxRaycastDistance = 100f;
    
    private PixelGrid _parentGrid;
    private Mesh _mesh;
    
    private void Start()
    {
        _parentGrid = GetComponentInParent<PixelGrid>();
        _mesh = GetComponent<MeshFilter>().mesh;
    }
    
    public void PaintPixel()
    {
        Ray viewingRay = new Ray();
        viewingRay.origin = _parentGrid.dummyCameraPosition;
        viewingRay.direction = transform.position - _parentGrid.dummyCameraPosition;
        
        if (Physics.Raycast(viewingRay, out RaycastHit hitInfo, maxRaycastDistance, _parentGrid.targetMask))
        {
            Color32[] newColors32 = new Color32[_mesh.vertexCount];

            Color32 hitColor = hitInfo.transform.gameObject.GetComponent<Renderer>().material.color;
            
            Debug.DrawLine(_parentGrid.dummyCameraPosition, hitInfo.point, hitColor, 1);
            
            for (int vertex = 0; vertex < _mesh.vertexCount; vertex++)
            {
                newColors32[vertex] = hitColor;
            }

            _mesh.colors32 = newColors32;
        }

        else
        {
            Vector3 missEndpoint = viewingRay.origin + (maxRaycastDistance * viewingRay.direction.normalized);
            Debug.DrawLine(_parentGrid.dummyCameraPosition, missEndpoint, Color.white, 1);
        }
    }

    public void ResetColor()
    {
        Color32[] newColors32 = new Color32[_mesh.vertexCount];
        
        for (int vertex = 0; vertex < _mesh.vertexCount; vertex++)
        {
            newColors32[vertex] = Color.white;
        }

        _mesh.colors32 = newColors32;
    }
}
