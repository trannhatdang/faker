using System;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Item : MonoBehaviour
{
    [SerializeField] ItemData itemdata;
    void Awake()
    {
        
    }

    public Sprite getIcon() {return itemdata.Icon;}
    public string getItemName() {return itemdata.ItemName;}
    public ItemData GetItemData() {return itemdata;}

}