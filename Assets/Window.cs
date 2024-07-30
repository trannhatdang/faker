using UnityEngine;

public class Window : MonoBehaviour
{
    public bool isClosed {get; private set;}
    SpriteRenderer spr;
    [SerializeField] Sprite open;
    [SerializeField] Sprite closed;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeWindow(bool value)
    {
        if(value)
        {
            spr.sprite = open;
        }
        else spr.sprite = closed;
        isClosed = value;
    }
}
