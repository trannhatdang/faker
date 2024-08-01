using UnityEngine;
using UnityEngine.UIElements;

public class UIInventorySlot : VisualElement
{
    public ItemData itemData {get; private set;}
    VisualElement IconContainer;
    public UIInventorySlot(GameObject item)
    {
        IconContainer = new VisualElement();
        Add(IconContainer);

        if(item != null) 
        {
            itemData = item.GetComponent<Item>().GetItemData();
        }
        else
        {
            itemData = null;
        }
        
        IconContainer.style.backgroundImage = Background.FromSprite(itemData.Icon);
        IconContainer.AddToClassList("slotIcon");
        AddToClassList("slotContainer");

        RegisterCallback<PointerDownEvent>(OnPointerDown);
    }
    public UIInventorySlot()
    {
        IconContainer = new VisualElement();
        Add(IconContainer);
        
        IconContainer.AddToClassList("slotIcon");
        AddToClassList("slotContainer");

        RegisterCallback<PointerDownEvent>(OnPointerDown);
    }
    public void HoldItem(ItemData in_item)
    {
        itemData = in_item;
        IconContainer.style.backgroundImage = Background.FromSprite(itemData.Icon);
    }
    public void DropItem()
    {
        itemData = null;
        IconContainer.style.backgroundImage = null;
    }
    void OnPointerDown(PointerDownEvent evt)
    {
        if (evt.button != 0 || itemData == null)
        {
            return;
        }

        IconContainer.style.backgroundImage = null;

        //Start the drag
        GameManager.inventoryUIController.StartDrag(evt.position, this);
    }
    public void RestoreImage()
    {
        IconContainer.style.backgroundImage = Background.FromSprite(itemData.Icon);
    }
}