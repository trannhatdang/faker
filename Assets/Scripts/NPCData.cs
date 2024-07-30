using Unity.VisualScripting.FullSerializer;
using UnityEngine;
    
    
[CreateAssetMenu(fileName = "NPCData", menuName = "NPC/NPCData")]
public class NPCData : ScriptableObject
{
    public string NPCName;
    public string NPCDescription;
    public Sprite NPCSprite;
    public float workPower;
    public float WindowDistance;
    public float ChanceToClose;
    public float ChanceToSlack;
}