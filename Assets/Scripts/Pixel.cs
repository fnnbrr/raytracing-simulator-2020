using UnityEngine;

public class Pixel : MonoBehaviour
{
    private PixelGrid _parentGrid;
    private Mesh _mesh;
    
    private void Start()
    {
        _parentGrid = GetComponentInParent<PixelGrid>();
        _mesh = GetComponent<MeshFilter>().mesh;
    }
    
    public void PaintPixel()
    {
        if (Physics.Linecast(_parentGrid.mainCameraPosition, transform.position, 
            out RaycastHit hitInfo, _parentGrid.targetMask))
        {
            Color32[] newColors32 = new Color32[_mesh.vertexCount];

            Color32 hitColor = hitInfo.transform.gameObject.GetComponent<Renderer>().material.color;
            
            Debug.DrawLine(_parentGrid.mainCameraPosition, transform.position, hitColor, 1);
            
            for (int vertex = 0; vertex < _mesh.vertexCount; vertex++)
            {
                newColors32[vertex] = hitColor;
            }

            _mesh.colors32 = newColors32;
        }
    }
}
