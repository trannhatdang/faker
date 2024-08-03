using Unity.VisualScripting.FullSerializer;
using UnityEngine;
    
    
[CreateAssetMenu(fileName = "MapData", menuName = "MapData")]
public class MapData : ScriptableObject
{
    public float upBorder;
    public float rightBorder;
    public float downBorder;
    public float leftBorder;
    public float maxZoom;
    public float upOutsideBorder;
    public float rightOutsideBorder;
    public float downOutsideBorder;
    public float leftOutsideBorder;
}