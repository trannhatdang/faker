using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using System.ComponentModel;
using UnityEditor.Search;
public class InventoryUIController : MonoBehaviour
{
    [SerializeField] Inventory inventoryData;
    static bool isDragging = false;
    static bool isInContextMenu = false;
    static UIInventorySlot m_OriginalSlot;
    //UI Stuff Down here
    VisualElement root;
    VisualElement container;
    VisualElement m_SlotContainer;
    VisualElement m_GhostSprite;
    VisualElement m_ContextMenu;
    UIInventorySlot selectingSlot;
    Button exitInventory;
    List<UIInventorySlot> UIItems = new List<UIInventorySlot>();
    void Awake()
    {        
        root = GetComponent<UIDocument>().rootVisualElement;

        m_SlotContainer = root.Q<VisualElement>("SlotContainer");
        container = root.Q<VisualElement>("Container");
        exitInventory = root.Q<Button>("ExitInventory");
        m_ContextMenu = root.Q<VisualElement>("ContextMenu");

        for (int i = 0; i < 50; i++)
        {
            UIInventorySlot item = null;
            if(inventoryData.items[i] != null) item = new UIInventorySlot(inventoryData.items[i]);
            else item = new UIInventorySlot();
            
            UIItems.Add(item);
            m_SlotContainer.Add(item);
        }        

        m_GhostSprite = root.Query<VisualElement>("GhostIcon");

        m_GhostSprite.style.display = DisplayStyle.None;
        container.style.display = DisplayStyle.None;
        m_ContextMenu.style.display = DisplayStyle.None;

        exitInventory.clicked += OnExitClicked; 

        m_GhostSprite.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        m_GhostSprite.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }
    void Start()
    {
        if(GameManager.inventoryUIController != null && GameManager.inventoryUIController != this)
        {
            Destroy(this.gameObject);
        }

        GameManager.manager.OnChangeInventory += OnInventoryChanged;
    }
    public void StartDrag(Vector3 position, UIInventorySlot originalSlot)
    {
        isDragging = true;
        m_OriginalSlot = originalSlot;
        m_GhostSprite.style.top = position.y - m_GhostSprite.layout.height / 2 - 64;
        m_GhostSprite.style.left = position.x - m_GhostSprite.layout.width / 2 - 64;

        m_GhostSprite.style.backgroundImage = Background.FromSprite(m_OriginalSlot.itemData.Icon);

        m_GhostSprite.style.display = DisplayStyle.Flex;
    }
    void Update()
    {
        
    }
    void OnPointerUp(PointerUpEvent evt)
    {
        if (!isDragging)
        {
            return;
        }

        //Check to see if they are dropping the ghost icon over any inventory slots.
        IEnumerable<UIInventorySlot> slots = UIItems.Where(x => 
           x.worldBound.Overlaps(m_GhostSprite.worldBound));

        //Found at least one
        if (slots.Count() != 0)
        {
            UIInventorySlot closestSlot = slots.OrderBy(x => Vector2.Distance
           (x.worldBound.position, m_GhostSprite.worldBound.position)).First();

           if(closestSlot == m_OriginalSlot) m_OriginalSlot.RestoreImage();
           else
           {            
            //Set the new inventory slot with the data
            closestSlot.HoldItem(m_OriginalSlot.itemData);
            
            //Clear the original slot
            m_OriginalSlot.DropItem();
           }
        }
        //Didn't find any (dragged off the window)
        else
        {
            m_OriginalSlot.RestoreImage();
        }

        //Clear dragging related visuals and data
        isDragging = false;
        m_OriginalSlot = null;
        m_GhostSprite.style.display = DisplayStyle.None;
    }
    void OnPointerMove(PointerMoveEvent evt)
    {
        if(!isDragging) return;

        m_GhostSprite.style.top = evt.position.y - m_GhostSprite.layout.height / 2;
        m_GhostSprite.style.left = evt.position.x - m_GhostSprite.layout.width / 2;
    }
    public void OpenInventory()
    {
        container.style.display = DisplayStyle.Flex;
    }
    void OnInventoryChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < 50; i++)
        {
            UIInventorySlot item;
            if(inventoryData.items[i] != null) item = new UIInventorySlot(inventoryData.items[i]);
            else item = new UIInventorySlot();
            
            UIItems.Add(item);
            m_SlotContainer.Add(item);
        }
    }
    void OnExitClicked()
    {
        CloseContextMenu();
        container.style.display = DisplayStyle.None;
    }
    public void OpenContextMenu(Vector3 position, UIInventorySlot slot)
    {
        isInContextMenu = true;
        
        m_ContextMenu.style.display = DisplayStyle.Flex;

        m_ContextMenu.style.top = position.y - m_ContextMenu.layout.height / 2 - 64;
        m_ContextMenu.style.left = position.x - m_ContextMenu.layout.width / 2 - 64;

        selectingSlot = slot;

        m_ContextMenu.Q<Button>("PlaceInWorld").clicked += e_OnPlaceInWorld;
        m_ContextMenu.Q<Button>("Sell").clicked += e_OnSell;
    }
    public void CloseContextMenu()
    {
        isInContextMenu = false;

        m_ContextMenu.Q<Button>("PlaceInWorld").clicked -= e_OnPlaceInWorld;
        m_ContextMenu.Q<Button>("Sell").clicked -= e_OnSell;
        selectingSlot = null;

        m_ContextMenu.style.display = DisplayStyle.None;
    }
    public bool getContextStatus() {return isInContextMenu;}
    void e_OnPlaceInWorld()
    {
        OnPlaceInWorld(selectingSlot);
    }
    void e_OnSell()
    {
        OnSell(selectingSlot);
    }
    void OnPlaceInWorld(UIInventorySlot slot)
    {
        Debug.Log(slot.itemData.ItemName);
        Player.player.GetComponent<Interact>().PickUp(Instantiate(Resources.Load<GameObject>("Items/Prefabs/" + slot.itemData.name)));
        slot.DropItem();
        CloseContextMenu();
        container.style.display = DisplayStyle.None;
    }
    void OnSell(UIInventorySlot slot)
    {
        GameManager.manager.gameState.Money += slot.itemData.Value;
        slot.DropItem();
        CloseContextMenu();
        container.style.display = DisplayStyle.None;
    }
}
