using Unity.VisualScripting;
using UnityEngine;

public class UI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Transform[] currentChildren;
    void Awake()
    {
        currentChildren = this.gameObject.GetComponentsInChildren<Transform>(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUIActive(string name, bool value)
    {
        GameObject toActive = this.gameObject;
        foreach(Transform tfs in currentChildren)
        {
            if(tfs.gameObject.name == name) toActive = tfs.gameObject; 
        }

        if(toActive == this.gameObject) 
        {
            Debug.Log("Loi o setUIActive trong UI.cs");
        };

        toActive.gameObject.SetActive(value);
    }
}
