using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
    
public class FactoriesListController
{
    // UXML template for list entries
    VisualTreeAsset m_ListEntryTemplate;
    // UI element references
    ListView m_FactoriesList;
    Label m_FactoriesDescription;
    Label m_FactoriesName;
    VisualElement m_FactoriesSprite;    
    List<FactoriesData> m_AllFactories;   
    VisualElement m_detailsPanel;
    Button m_btn;
    public void InitializeCharacterList(VisualElement root, VisualTreeAsset listElementTemplate)
    {
        EnumerateAllItems();    
        
        m_ListEntryTemplate = listElementTemplate;    
         
        m_FactoriesList = root.Q<ListView>("factories-list");
    
        // Store references to the selected character info elements
        m_FactoriesDescription = root.Q<Label>("factory-description");
        m_FactoriesName = root.Q<Label>("factory-name");
        m_FactoriesSprite = root.Q<VisualElement>("factory-sprite");
        m_detailsPanel = root.Q<VisualElement>("right-container");
        m_btn = root.Q<Button>(className: "factory-button");

        m_detailsPanel.style.display = DisplayStyle.None;  

        FillItemList();
    
        // Register to get a callback when an item is selected
        m_FactoriesList.selectionChanged += OnCharacterSelected;
    }
    
    void EnumerateAllItems()
    {
        m_AllFactories = new List<FactoriesData>();
        m_AllFactories.AddRange(Resources.LoadAll<FactoriesData>("Factories"));
    }
    
    void FillItemList()
    {
        // Set up a make item function for a list entry
        m_FactoriesList.makeItem = () =>
        {
            // Instantiate the UXML template for the entry
            var newListEntry = m_ListEntryTemplate.Instantiate();
    
            // Instantiate a controller for the data
            var newListEntryLogic = new FactoriesListEntryController();
    
            // Assign the controller script to the visual element
            newListEntry.userData = newListEntryLogic;
    
            // Initialize the controller script
            newListEntryLogic.SetVisualElement(newListEntry);
    
            // Return the root of the instantiated visual tree
            return newListEntry;
        };
    
        // Set up bind function for a specific list entry
        m_FactoriesList.bindItem = (item, index) =>
        {
            (item.userData as FactoriesListEntryController)?.SetCharacterData(m_AllFactories[index]);
        };
    
        // Set a fixed item height matching the height of the item provided in makeItem. 
        // For dynamic height, see the virtualizationMethod property.
        m_FactoriesList.fixedItemHeight = 45;
    
        // Set the actual item's source list/array
        m_FactoriesList.itemsSource = m_AllFactories;
    }
    
    void OnCharacterSelected(IEnumerable<object> selectedItems)
    {
        // Get the currently selected item directly from the ListView
        var selectedCharacter = m_FactoriesList.selectedItem as FactoriesData;
        
    
        // Handle none-selection (Escape to deselect everything)
        if (selectedCharacter == null)
        {
            // Clear
            m_detailsPanel.style.display = DisplayStyle.None;            
            return;
        }

        m_detailsPanel.style.display = DisplayStyle.Flex;  
    
        // Fill in character details
        m_FactoriesDescription.text = selectedCharacter.FactoriesDescriptions.ToString();
        m_FactoriesName.text = selectedCharacter.FactoriesName;
        m_FactoriesSprite.style.backgroundImage = new StyleBackground(selectedCharacter.Icon);
        m_btn.name = selectedCharacter.name;
    }
}