using UnityEngine;
using UnityEngine.UIElements;

public class FillShopData : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] VisualTreeAsset m_ListEntryTemplate;   
    [SerializeField] VisualTreeAsset m_ItemListEntryTemplate;
    [SerializeField] VisualTreeAsset m_FactoriesListEntryTemplate;
    [SerializeField] VisualTreeAsset m_UpgradesListEntryTemplate;
    void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();
    
        // Initialize the character list controller
        var WorkersListController = new NPCListController();
        var ItemsListController = new ItemsListController();
        // var UpgradesListController = new UpgradesListController();
        // var FactoriesListController = new FactoriesListController();
        WorkersListController.InitializeCharacterList(uiDocument.rootVisualElement.Q<Tab>("Workers"), m_ListEntryTemplate);
        ItemsListController.InitializeCharacterList(uiDocument.rootVisualElement.Q<Tab>("Items"), m_ItemListEntryTemplate);
        // FactoriesListController.InitializeCharacterList(uiDocument.rootVisualElement.Q<Tab>("Factories"), m_FactoriesListEntryTemplate);
        // UpgradesListController.InitializeCharacterList(uiDocument.rootVisualElement.Q<Tab>("Upgrades"), m_UpgradesListEntryTemplate);
    }
}
