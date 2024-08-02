using UnityEngine;
using UnityEngine.UIElements;

public class MeshController : MonoBehaviour
{
    public DrawingMesh drawingMesh;
    void OnEnable()
    {        
        VisualElement root = gameObject.GetComponent<UIDocument>().rootVisualElement;
        root.Q<TexturedElement>("TexturedElement").mesh = drawingMesh.mesh;
    }
}
