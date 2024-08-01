using Unity.VisualScripting.FullSerializer;
using UnityEngine;
    
    
[CreateAssetMenu(fileName = "MapData", menuName = "MapData")]
public class MapData : ScriptableObject
{
    public float leftBorder;
    public float rightBorder;
    public float upBorder;
    public float downBorder;
}