using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public static Player player {get; private set;} 
    void Awake()
    {
        if(player != null && player != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            player = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
