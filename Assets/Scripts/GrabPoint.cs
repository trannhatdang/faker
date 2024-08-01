using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class GrabPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Collider2D col;
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Wall")) transform.position = transform.parent.transform.position;
    }
}
