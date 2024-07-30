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
        if(name == "RauHire")
        {
            IEnumerator couroutine = GameManager.manager.Hire("ChuBanRau");
            StartCoroutine(couroutine);
        }
    }
}
