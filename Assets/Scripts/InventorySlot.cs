using UnityEngine;
using UnityEngine.UIElements;

public class InventorySlot : VisualElement
{
    public Sprite Icon;
    public string ItemGuid = "";
    public InventorySlot(GameObject item)
    {
        VisualElement IconContainer = new VisualElement();
        if(item == null)
        {
            Icon = null;
        }
        else 
        {
            Icon = item.GetComponent<Item>().image;
        }
        
        IconContainer.style.backgroundImage = Background.FromSprite(Icon);
        IconContainer.AddToClassList("slotIcon");
        AddToClassList("slotContainer");
    }
}