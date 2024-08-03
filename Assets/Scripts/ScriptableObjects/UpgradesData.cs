using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class UpgradesData : ScriptableObject
{
    public string UpgradesName;
    public Sprite Icon;
    public int Value;
    public string UpgradesDescription;
}