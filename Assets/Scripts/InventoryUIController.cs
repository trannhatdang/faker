using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIController : MonoBehaviour
{
    private VisualElement root;
    private VisualElement m_SlotContainer;
    private List<InventorySlot> items = new List<InventorySlot>();
    [SerializeField] Inventory inventoryData;
    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        m_SlotContainer = root.Q<VisualElement>("SlotContainer");

        for(int i = 0; i < inventoryData.items.Count; i++)
        {
            InventorySlot item = new InventorySlot(inventoryData.items[i]);

            items.Add(item);
            m_SlotContainer.Add(item);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateInventory()
    {
        for(int i = 0; i < items.Count; i++)
        {
            
        }
    }
}
