using DG.Tweening;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Camera mainCamera;
    MapData mapData;
    int mDelta = 10;    
    void Start()
    {   
        mapData = GameManager.manager.getMapData();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mousePosition.x >= Screen.width - mDelta && transform.position.x + mainCamera.orthographicSize * mainCamera.aspect <= mapData.rightOutsideBorder)
        {
            transform.position += Vector3.right * Time.deltaTime * 5f;
        }

        if(Input.mousePosition.x <= 0 + mDelta && transform.position.x - mainCamera.orthographicSize * mainCamera.aspect >= mapData.leftOutsideBorder)
        {
            transform.position += Vector3.left * Time.deltaTime * 5f;
        }

        if(Input.mousePosition.y >= Screen.height - mDelta && transform.position.y + mainCamera.orthographicSize <= mapData.upOutsideBorder)
        {
            transform.position += Vector3.up * Time.deltaTime * 5f;
        }

        if(Input.mousePosition.y <= 0 + mDelta && transform.position.y - mainCamera.orthographicSize >= mapData.downOutsideBorder)
        {
            transform.position += Vector3.down * Time.deltaTime * 5f;
        }

        float size = Camera.main.orthographicSize;

        size -= Input.mouseScrollDelta.y * 0.5f;

        if(size > .5 && size < 2.61) Camera.main.orthographicSize = size; 
    }
}
