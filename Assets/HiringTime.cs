using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HiringTime : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    List<Button> HireButtons;

    void Start()
    {
        var uiDocument = GetComponent<UIDocument>().rootVisualElement;
        HireButtons = uiDocument.Query<Button>(className: "hire-button").ToList();

        
    }

    public void startHire(string name)
    {
        if(name == "RauHire")
        {
            
        }
    }
}
