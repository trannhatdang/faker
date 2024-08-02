using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class Overlay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    VisualElement overlay;
    VisualElement ShopPopup; 
    List<VisualElement> ControlOverlays = new List<VisualElement>(); 
    Button ShopButton;
    Button CloseShop;
    List<Button> HireButtons;
    void Start()
    {
        overlay = GetComponent<UIDocument>().rootVisualElement;

        ShopPopup = overlay.Q<VisualElement>("ShopMenu");
        CloseShop = overlay.Q<Button>("ExitShop");
        ShopButton = overlay.Q<Button>("Shop");
        HireButtons = overlay.Query<Button>(className: "hire-button").ToList();

        ShopButton.clicked += OnShopClick;
        CloseShop.clicked += OnShopExitClick;             

        foreach(Button button in HireButtons)
        {
            button.clicked += () =>
            {
                OnHire(button.name);
            };
        }        

        ShopPopup.style.display = DisplayStyle.None;

        ControlOverlays.Add(overlay.Q<VisualElement>("FirstButton")); 
        ControlOverlays.Add(overlay.Q<VisualElement>("SecondButton")); 
        ControlOverlays.Add(overlay.Q<VisualElement>("ThirdButton")); 

        for(int i = 0; i < ControlOverlays.Count; i++)
        {
            ControlOverlays[i].style.display = DisplayStyle.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnShopClick()
    {
        ShopPopup.style.display = DisplayStyle.Flex;
    }

    void OnShopExitClick()
    {
        ShopPopup.style.display = DisplayStyle.None;
    }

    void OnHire(string name)
    {
        GameManager.manager.Hire(name);
    }

}
