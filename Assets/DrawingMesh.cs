using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UIElements;

public class DrawingMesh : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Mesh m_mesh;
    [SerializeField] private Transform debugVisual1;
    [SerializeField] private Transform debugVisual2;
    Vector3 lastMousePosition;
    Camera mainCam;
    [SerializeField] UIDocument uiDocument;
    public Mesh mesh 
    {
        get => m_mesh;
        set 
        {
            m_mesh = value;
        }
    }    
    void Awake()
    {       
        
    }
    private void OnEnable()
    {       

        // mainCam = Camera.main;
        // m_mesh = new Mesh();
        
        // Vector3[] vertices = new Vector3[4];
        // Vector2[] uv = new Vector2[4];
        // int[] triangles = new int[6];

        // vertices[0] = mainCam.ScreenToWorldPoint(Input.mousePosition);
        // vertices[1] = mainCam.ScreenToWorldPoint(Input.mousePosition);
        // vertices[2] = mainCam.ScreenToWorldPoint(Input.mousePosition);
        // vertices[3] = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // uv[0] = Vector2.zero;
        // uv[1] = Vector2.zero;
        // uv[2] = Vector2.zero;
        // uv[3] = Vector2.zero;

        // triangles[0] = 0;
        // triangles[1] = 3;
        // triangles[2] = 1;

        // triangles[3] = 1;
        // triangles[4] = 3;
        // triangles[5] = 2;

        // m_mesh.vertices = vertices;
        // m_mesh.uv = uv;
        // m_mesh.triangles = triangles;
        // m_mesh.MarkDynamic();
        
        // lastMousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // GetComponent<MeshFilter>().mesh = mesh;
    } 
    // Update is called once per frame
    void Update()
    {       
        // if(Input.GetMouseButton(0))
        // {
        //     if(Vector3.Distance(mainCam.ScreenToWorldPoint(Input.mousePosition),lastMousePosition) < .1f) return; 

        //     Vector3[] vertices = new Vector3[m_mesh.vertices.Length + 2];
        //     Vector2[] uv = new Vector2[m_mesh.uv.Length + 2];
        //     int[] triangles = new int[m_mesh.triangles.Length + 6];

        //     m_mesh.vertices.CopyTo(vertices, 0);
        //     m_mesh.uv.CopyTo(uv, 0);
        //     m_mesh.triangles.CopyTo(triangles, 0);

        //     int vIndex = vertices.Length - 4;
        //     int vIndex0 = vIndex + 0;
        //     int vIndex1 = vIndex + 1;
        //     int vIndex2 = vIndex + 2;
        //     int vIndex3 = vIndex + 3;

        //     Vector3 mouseForwardVector = (mainCam.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition).normalized;
        //     Vector3 normal2D = new Vector3(0,0,-1f);
        //     float lineThickness = .1f;
        //     Vector3 newVertexUp = mainCam.ScreenToWorldPoint(Input.mousePosition) + Vector3.Cross(mouseForwardVector, normal2D) * lineThickness;
        //     Vector3 newVertexDown = mainCam.ScreenToWorldPoint(Input.mousePosition) + Vector3.Cross(mouseForwardVector, normal2D * -1f) * lineThickness;
        //     newVertexUp.z = 0;
        //     newVertexDown.z = 0;

        //     // debugVisual1.position = newVertexUp;
        //     // debugVisual2.position = newVertexDown;

        //     vertices[vIndex2] = newVertexUp;
        //     vertices[vIndex3] = newVertexDown;

        //     uv[vIndex2] = Vector2.zero;
        //     uv[vIndex3] = Vector2.zero;
            
        //     int tIndex = triangles.Length - 6;

        //     triangles[tIndex + 0] = vIndex0;
        //     triangles[tIndex + 1] = vIndex2;
        //     triangles[tIndex + 2] = vIndex1;

        //     triangles[tIndex + 3] = vIndex1;
        //     triangles[tIndex + 4] = vIndex2;
        //     triangles[tIndex + 5] = vIndex3;

        //     m_mesh.vertices = vertices;
        //     m_mesh.uv = uv;
        //     m_mesh.triangles = triangles;

        //     lastMousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        // }           
    }
}
