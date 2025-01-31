using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
    
public class NPCListController
{
    // UXML template for list entries
    VisualTreeAsset m_ListEntryTemplate;
    // UI element references
    ListView m_NPCList;
    Label m_NPCDescription;
    Label m_NPCName;
    VisualElement m_NPCSprite;    
    List<NPCData> m_AllNPC;   
    VisualElement m_detailsPanel;
    Button m_btn;
    public void InitializeCharacterList(VisualElement root, VisualTreeAsset listElementTemplate)
    {
        EnumerateAllItems();    
        
        m_ListEntryTemplate = listElementTemplate;    
         
        m_NPCList = root.Q<ListView>("npc-list");
    
        // Store references to the selected character info elements
        m_NPCDescription = root.Q<Label>("npc-description");
        m_NPCName = root.Q<Label>("npc-name");
        m_NPCSprite = root.Q<VisualElement>("npc-sprite");
        m_detailsPanel = root.Q<VisualElement>("right-container");
        m_btn = root.Q<Button>(className: "hire-button");

        m_detailsPanel.style.display = DisplayStyle.None;  

        FillItemList();
    
        // Register to get a callback when an item is selected
        m_NPCList.selectionChanged += OnCharacterSelected;
    }
    
    void EnumerateAllItems()
    {
        m_AllNPC = new List<NPCData>();
        m_AllNPC.AddRange(Resources.LoadAll<NPCData>("NPCs"));
    }
    
    void FillItemList()
    {
        // Set up a make item function for a list entry
        m_NPCList.makeItem = () =>
        {
            // Instantiate the UXML template for the entry
            var newListEntry = m_ListEntryTemplate.Instantiate();
    
            // Instantiate a controller for the data
            var newListEntryLogic = new NPCListEntryController();
    
            // Assign the controller script to the visual element
            newListEntry.userData = newListEntryLogic;
    
            // Initialize the controller script
            newListEntryLogic.SetVisualElement(newListEntry);
    
            // Return the root of the instantiated visual tree
            return newListEntry;
        };
    
        // Set up bind function for a specific list entry
        m_NPCList.bindItem = (item, index) =>
        {
            (item.userData as NPCListEntryController)?.SetCharacterData(m_AllNPC[index]);
        };
    
        // Set a fixed item height matching the height of the item provided in makeItem. 
        // For dynamic height, see the virtualizationMethod property.
        m_NPCList.fixedItemHeight = 45;
    
        // Set the actual item's source list/array
        m_NPCList.itemsSource = m_AllNPC;
    }
    
    void OnCharacterSelected(IEnumerable<object> selectedItems)
    {
        // Get the currently selected item directly from the ListView
        var selectedCharacter = m_NPCList.selectedItem as NPCData;
        
    
        // Handle none-selection (Escape to deselect everything)
        if (selectedCharacter == null)
        {
            // Clear
            m_detailsPanel.style.display = DisplayStyle.None;            
            return;
        }

        m_detailsPanel.style.display = DisplayStyle.Flex;  
    
        // Fill in character details
        m_NPCDescription.text = selectedCharacter.NPCDescription.ToString();
        m_NPCName.text = selectedCharacter.NPCName;
        m_NPCSprite.style.backgroundImage = new StyleBackground(selectedCharacter.NPCSprite);
        m_btn.name = selectedCharacter.name;
    }
}