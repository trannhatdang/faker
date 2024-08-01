using Microsoft.Unity.VisualStudio.Editor;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class Item : MonoBehaviour
{
    public Sprite image {get; private set;}
    [SerializeField] string ItemName;
    [SerializeField] ItemData itemdata;
    void Awake()
    {
        image = itemdata.Icon;
    }
}