using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created]
    private SpriteRenderer spr;
    [SerializeField] Sprite openSprite;
    [SerializeField] Sprite closeSprite;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDoor(bool value)
    {
        if(value) spr.sprite = openSprite;
        else spr.sprite = closeSprite; 
    }
}
