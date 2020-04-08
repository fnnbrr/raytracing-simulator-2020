using System.Collections;
using UnityEngine;

public class Pixel : MonoBehaviour
{
    public float maxRaycastDistance = 100f;
    
    private PixelGrid _parentGrid;
    private Mesh _mesh;
    private LineRenderer _lineRenderer;
    
    private void Start()
    {
        _parentGrid = GetComponentInParent<PixelGrid>();
        _mesh = GetComponent<MeshFilter>().mesh;
        
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.widthMultiplier = 0.01f;
        _lineRenderer.enabled = false;
    }
    
    public void PaintPixel()
    {
        Ray viewingRay = new Ray
        {
            origin = _parentGrid.dummyCameraPosition,
            direction = transform.position - _parentGrid.dummyCameraPosition
        };

        Vector3 lineEndpoint = viewingRay.origin + (maxRaycastDistance * viewingRay.direction.normalized);
        Color lineColor = Color.gray;
        
        if (Physics.Raycast(viewingRay, out RaycastHit hitInfo, maxRaycastDistance, _parentGrid.targetMask))
        {
            Color32[] newColors32 = new Color32[_mesh.vertexCount];

            lineEndpoint = hitInfo.point;
            lineColor = hitInfo.transform.gameObject.GetComponent<Renderer>().material.color;
            
            for (int vertex = 0; vertex < _mesh.vertexCount; vertex++)
            {
                newColors32[vertex] = lineColor;
            }

            _mesh.colors32 = newColors32;
            
            StartCoroutine(ClearLineAfterDelay(5));
        }
        else
        {
            StartCoroutine(ClearLineAfterDelay());
        }

        Vector3[] positions = {_parentGrid.dummyCameraPosition, lineEndpoint};
        _lineRenderer.SetPositions(positions);
        _lineRenderer.startColor = lineColor;
        _lineRenderer.endColor = lineColor;
        _lineRenderer.enabled = true;
    }
    
    public IEnumerator ClearLineAfterDelay(float delay=1)
    {
        yield return new WaitForSeconds(delay);

        _lineRenderer.enabled = false;
    }

    public void ResetColor()
    {
        Color32[] newColors32 = new Color32[_mesh.vertexCount];
        
        for (int vertex = 0; vertex < _mesh.vertexCount; vertex++)
        {
            newColors32[vertex] = Color.white;
        }

        _mesh.colors32 = newColors32;
        
        StopAllCoroutines();
        
        _lineRenderer.enabled = false;
    }
}
