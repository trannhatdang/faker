using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
    
public class UpgradesListController
{
    // UXML template for list entries
    VisualTreeAsset m_ListEntryTemplate;
    // UI element references
    ListView m_UpgradesList;
    Label m_UpgradesDescription;
    Label m_UpgradesName;
    VisualElement m_UpgradesSprite;    
    List<ItemData> m_AllUpgrades;   
    VisualElement m_detailsPanel;
    Button m_btn;
    public void InitializeCharacterList(VisualElement root, VisualTreeAsset listElementTemplate)
    {
        EnumerateAllItems();    
        
        m_ListEntryTemplate = listElementTemplate;    
         
        m_UpgradesList = root.Q<ListView>("upgrades-list");
    
        // Store references to the selected character info elements
        m_UpgradesDescription = root.Q<Label>("upgrades-description");
        m_UpgradesName = root.Q<Label>("upgrades-name");
        m_UpgradesSprite = root.Q<VisualElement>("upgrades-sprite");
        m_detailsPanel = root.Q<VisualElement>("right-container");
        m_btn = root.Q<Button>(className: "upgrades-button");

        m_detailsPanel.style.display = DisplayStyle.None;  

        FillItemList();
    
        // Register to get a callback when an item is selected
        m_UpgradesList.selectionChanged += OnCharacterSelected;
    }
    
    void EnumerateAllItems()
    {
        m_AllUpgrades = new List<ItemData>();
        m_AllUpgrades.AddRange(Resources.LoadAll<ItemData>("Upgrades"));
    }
    
    void FillItemList()
    {
        // Set up a make item function for a list entry
        m_UpgradesList.makeItem = () =>
        {
            // Instantiate the UXML template for the entry
            var newListEntry = m_ListEntryTemplate.Instantiate();
    
            // Instantiate a controller for the data
            var newListEntryLogic = new ItemsListEntryController();
    
            // Assign the controller script to the visual element
            newListEntry.userData = newListEntryLogic;
    
            // Initialize the controller script
            newListEntryLogic.SetVisualElement(newListEntry);
    
            // Return the root of the instantiated visual tree
            return newListEntry;
        };
    
        // Set up bind function for a specific list entry
        m_UpgradesList.bindItem = (item, index) =>
        {
            (item.userData as ItemsListEntryController)?.SetCharacterData(m_AllUpgrades[index]);
        };
    
        // Set a fixed item height matching the height of the item provided in makeItem. 
        // For dynamic height, see the virtualizationMethod property.
        m_UpgradesList.fixedItemHeight = 45;
    
        // Set the actual item's source list/array
        m_UpgradesList.itemsSource = m_AllUpgrades;
    }
    
    void OnCharacterSelected(IEnumerable<object> selectedItems)
    {
        // Get the currently selected item directly from the ListView
        var selectedCharacter = m_UpgradesList.selectedItem as ItemData;
        
    
        // Handle none-selection (Escape to deselect everything)
        if (selectedCharacter == null)
        {
            // Clear
            m_detailsPanel.style.display = DisplayStyle.None;            
            return;
        }

        m_detailsPanel.style.display = DisplayStyle.Flex;  
    
        // Fill in character details
        m_UpgradesDescription.text = selectedCharacter.itemDescription.ToString();
        m_UpgradesName.text = selectedCharacter.ItemName;
        m_UpgradesSprite.style.backgroundImage = new StyleBackground(selectedCharacter.Icon);
        m_btn.name = selectedCharacter.name;
    }
}