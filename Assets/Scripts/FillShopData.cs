using UnityEngine;
using UnityEngine.UIElements;

public class FillShopData : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    VisualTreeAsset m_ListEntryTemplate;    
    void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();
    
        // Initialize the character list controller
        var WorkersListController = new ItemListController();
        WorkersListController.InitializeCharacterList(uiDocument.rootVisualElement.Q<Tab>("Workers"), m_ListEntryTemplate);
    }
}
