using UnityEngine;

public abstract class CompositeNode : Node
{
    protected int CurrentChildIndex = 0;

    //constructor
    protected CompositeNode(string displayName, params Node[] childNodes)
    {
        Name = displayName;

        
    }
}
