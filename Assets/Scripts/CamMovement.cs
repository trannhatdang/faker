using DG.Tweening;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float leftBorder;
    [SerializeField] float rightBorder;
    [SerializeField] float upBorder;
    [SerializeField] float downBorder;
    int mDelta = 10;
    
    void Start()
    {   

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mousePosition.x >= Screen.width - mDelta && transform.position.x <= rightBorder)
        {
            transform.position += Vector3.right * Time.deltaTime * 5f;
        }

        if(Input.mousePosition.x <= 0 + mDelta && transform.position.x >= leftBorder)
        {
            transform.position += Vector3.left * Time.deltaTime * 5f;
        }

        if(Input.mousePosition.y >= Screen.height - mDelta && transform.position.y <= upBorder)
        {
            transform.position += Vector3.up * Time.deltaTime * 5f;
        }

        if(Input.mousePosition.y <= 0 + mDelta && transform.position.y >= downBorder)
        {
            transform.position += Vector3.down * Time.deltaTime * 5f;
        }

        float size = Camera.main.orthographicSize;

        size -= Input.mouseScrollDelta.y * 0.5f;

        if(size > 1.9 && size < 7) Camera.main.orthographicSize = size; 
    }
}
