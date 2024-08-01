using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int ID;
    public string ItemName;
    public Sprite Icon;
    public int Value;
}