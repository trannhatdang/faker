using UnityEngine;
using UnityEngine.UIElements;

public class ShopMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    VisualElement shopMenu;
    Button exit;
    void Awake()
    {
        shopMenu = gameObject.GetComponent<UIDocument>().rootVisualElement;

        shopMenu.style.display = DisplayStyle.None;
    }
    void OnEnable()
    {     
        exit = shopMenu.Q<Button>("ExitButton");
        if(exit == null) Debug.Log("khong co exit");
        exit.clicked += onExitButtonClicked;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onExitButtonClicked()
    {
        this.gameObject.SetActive(false);
    }
}
