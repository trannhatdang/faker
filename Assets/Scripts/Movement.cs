using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    Vector2 movement;    
    float Horizontal = 0;
    float Vertical = 0;
    public float LastHort {get; private set;}
    public float LastVert {get; private set;}
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal"); movement.x = Horizontal;
        Vertical = Input.GetAxisRaw("Vertical"); movement.y = Vertical;
        animator.SetFloat("Horizontal", Horizontal); 
        animator.SetFloat("Vertical", Vertical); 
        animator.SetFloat("Speed", movement.sqrMagnitude);  

        if(Horizontal == 1 || Horizontal == -1 || Vertical == 1 || Vertical == -1)
        {
            animator.SetFloat("LastHort", Horizontal);
            LastHort = Horizontal;
            animator.SetFloat("LastVert", Vertical);
            LastVert = Vertical;
        }
    }

    void FixedUpdate()
    {   
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
