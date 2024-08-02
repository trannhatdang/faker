using UnityEngine.UIElements;
using UnityEngine;
using Unity.Properties;
using UnityEditor;
using System.Security;
using JetBrains.Annotations;

[UxmlElement]
public partial class TexturedElement : VisualElement
{
    static readonly Vertex[] k_Vertices = new Vertex[4];
    static readonly ushort[] k_Indices = { 0, 1, 2, 2, 3, 0 };
    [SerializeField, DontCreateProperty] Mesh m_mesh = new Mesh();
    [UxmlAttribute, CreateProperty] public Mesh mesh
    {
        get => m_mesh;
        set
        {
            m_mesh = value;
            MarkDirtyRepaint();
        }
    }
    static TexturedElement()
    {
        k_Vertices[0].tint = Color.white;
        k_Vertices[1].tint = Color.white;
        k_Vertices[2].tint = Color.white;
        k_Vertices[3].tint = Color.white;

        k_Vertices[0].uv = new Vector2(0, 0);
        k_Vertices[1].uv = new Vector2(0, 1);
        k_Vertices[2].uv = new Vector2(1, 1);
        k_Vertices[3].uv = new Vector2(1, 0);
    }

    public TexturedElement()
    {
        generateVisualContent += OnGenerateVisualContent;
        m_Texture = GenerateTexture2D();
    }

    public int textureWidth;
    public int textureHeight;
    public Texture2D GenerateTexture2D(){
        Texture2D texture2D = new Texture2D(textureWidth, textureHeight);
        texture2D.SetPixels(mesh.colors);
        return texture2D;
    }

    Texture2D m_Texture;

    void OnGenerateVisualContent(MeshGenerationContext mgc)
    {
        Rect r = contentRect;
        if (r.width < 0.01f || r.height < 0.01f)
            return; // Skip rendering when too small.

        float left = 0;
        float right = r.width;
        float top = 0;
        float bottom = r.height;

        k_Vertices[0].position = new Vector3(left, bottom, Vertex.nearZ);
        k_Vertices[1].position = new Vector3(left, top, Vertex.nearZ);
        k_Vertices[2].position = new Vector3(right, top, Vertex.nearZ);
        k_Vertices[3].position = new Vector3(right, bottom, Vertex.nearZ);

        MeshWriteData mwd = mgc.Allocate(k_Vertices.Length, k_Indices.Length, m_Texture);
        mwd.SetAllVertices(k_Vertices);
        mwd.SetAllIndices(k_Indices);
    }
}