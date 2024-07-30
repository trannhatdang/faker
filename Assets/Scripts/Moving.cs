using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {     

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDrag()
    {
        rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
