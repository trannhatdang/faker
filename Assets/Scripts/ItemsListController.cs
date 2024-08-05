using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
    
public class ItemsListController
{
    // UXML template for list entries
    VisualTreeAsset m_ListEntryTemplate;
    // UI element references
    ListView m_ItemsList;
    Label m_ItemsDescription;
    Label m_ItemsName;
    VisualElement m_ItemsSprite;    
    List<ItemData> m_AllItems;   
    VisualElement m_detailsPanel;
    Button m_btn;
    public void InitializeCharacterList(VisualElement root, VisualTreeAsset listElementTemplate)
    {
        EnumerateAllItems();    
        
        m_ListEntryTemplate = listElementTemplate;    
         
        m_ItemsList = root.Q<ListView>("item-list");
    
        // Store references to the selected character info elements
        m_ItemsDescription = root.Q<Label>("item-description");
        m_ItemsName = root.Q<Label>("item-name");
        m_ItemsSprite = root.Q<VisualElement>("item-sprite");
        m_detailsPanel = root.Q<VisualElement>("right-container");
        m_btn = root.Q<Button>(className: "buy-button");

        m_detailsPanel.style.display = DisplayStyle.None;  

        FillItemList();
    
        // Register to get a callback when an item is selected
        m_ItemsList.selectionChanged += OnCharacterSelected;
    }
    
    void EnumerateAllItems()
    {
        m_AllItems = new List<ItemData>();
        m_AllItems.AddRange(Resources.LoadAll<ItemData>("Items"));
    }
    
    void FillItemList()
    {
        // Set up a make item function for a list entry
        m_ItemsList.makeItem = () =>
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
        m_ItemsList.bindItem = (item, index) =>
        {
            (item.userData as ItemsListEntryController)?.SetCharacterData(m_AllItems[index]);
        };
    
        // Set a fixed item height matching the height of the item provided in makeItem. 
        // For dynamic height, see the virtualizationMethod property.
        m_ItemsList.fixedItemHeight = 45;
    
        // Set the actual item's source list/array
        m_ItemsList.itemsSource = m_AllItems;
    }
    
    void OnCharacterSelected(IEnumerable<object> selectedItems)
    {
        // Get the currently selected item directly from the ListView
        var selectedCharacter = m_ItemsList.selectedItem as ItemData;
        
    
        // Handle none-selection (Escape to deselect everything)
        if (selectedCharacter == null)
        {
            // Clear
            m_detailsPanel.style.display = DisplayStyle.None;            
            return;
        }

        m_detailsPanel.style.display = DisplayStyle.Flex;  
    
        // Fill in character details
        m_ItemsDescription.text = selectedCharacter.itemDescription.ToString();
        m_ItemsName.text = selectedCharacter.ItemName;
        m_ItemsSprite.style.backgroundImage = new StyleBackground(selectedCharacter.Icon);
        m_btn.name = selectedCharacter.name;
    }
}