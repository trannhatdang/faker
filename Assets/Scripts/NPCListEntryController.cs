using UnityEngine.UIElements;
    
public class NPCListEntryController
{
    Label m_NameLabel;
    
    // This function retrieves a reference to the 
    // character name label inside the UI element.
    public void SetVisualElement(VisualElement visualElement)
    {
        m_NameLabel = visualElement.Q<Label>("npc-name");
    }
    
    // This function receives the character whose name this list 
    // element is supposed to display. Since the elements list 
    // in a `ListView` are pooled and reused, it's necessary to 
    // have a `Set` function to change which character's data to display.
    public void SetCharacterData(NPCData npcData)
    {
        m_NameLabel.text = npcData.NPCName;
    }
}