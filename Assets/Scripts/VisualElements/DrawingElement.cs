using UnityEngine;
using UnityEngine.UIElements;
using Unity.Properties;
using Unity.VisualScripting;
using Unity.Jobs;
using System;
using Unity.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEditor;
using System.Linq;

[UxmlElement]
 public partial class DrawingElement : VisualElement
 {
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
    public DrawingElement()
    {
        generateVisualContent += OnGenerateVisualContent;
    }
    void OnGenerateVisualContent(MeshGenerationContext mgc)
    {
        var indices = new NativeArray<ushort>();

        for(int i = 0; i < m_mesh.triangles.Length; i++)
        {
            indices.Append((ushort)m_mesh.triangles[i]); 
        }

        var vertices = new NativeArray<Vertex>();
        for(int i = 0; i < m_mesh.vertices.Length; i++)
        {
            vertices.Append(new Vertex {position = m_mesh.vertices[i], tint = Color.white}); 
        }

    
        MeshWriteData mwd = mgc.Allocate(vertices.Count(), indices.Count(), null);


    }
 }