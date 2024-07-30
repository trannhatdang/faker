using System;
using UnityEngine;
using UnityEngine.UIElements;

public class HoverUi : MonoBehaviour
{
    ProgressBar prog_bar {get; set;}
    [SerializeField] UIDocument uIDocument;
    [SerializeField] Transform transformtoFollow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        prog_bar = uIDocument.rootVisualElement.Q<ProgressBar>("ProgressBar");

        SetPosition();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SetPosition();
        SetSize();
    }

    void SetPosition()
    {
        Vector2 newPosition = RuntimePanelUtils.CameraTransformWorldToPanel(
            prog_bar.panel, transformtoFollow.position, Camera.main);

        prog_bar.transform.position = new Vector3(newPosition.x - prog_bar.layout.width / 2, newPosition.y);
    }

    void SetSize()
    {
        if(Camera.main.orthographicSize >= 2) prog_bar.style.display = DisplayStyle.None;
        else 
        {
            prog_bar.style.display = DisplayStyle.Flex;
            float size = Camera.main.orthographicSize;
            prog_bar.transform.scale = new Vector3(3 / size, 3 / size);
        }
    }
}
