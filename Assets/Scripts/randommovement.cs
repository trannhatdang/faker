using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;

public class randommovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float lerpSpeed = 5f;
    [SerializeField] float minX;
    [SerializeField] float minY;
    [SerializeField] float maxX;
    [SerializeField] float maxY;
    UnityEngine.Vector3 newPos;
    UnityEngine.Quaternion newRot;
    void Awake()
    {
        newPos = transform.position;
        newRot = transform.rotation;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = UnityEngine.Vector3.Lerp(transform.position, newPos, Time.deltaTime * lerpSpeed);

        if(UnityEngine.Vector3.Distance(transform.position, newPos) < 1f)
        {
            float xPos = Random.Range(minX, maxX);
            float yPos = Random.Range(minY, maxY);
            newPos = new UnityEngine.Vector3(xPos, yPos, -10);
        }
    }
}
