using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public abstract class CompositeNode : Node
{
    protected int CurrentChildIndex = 0;

    //constructor
    protected CompositeNode(string displayName, params Node[] childNodes)
    {
        Name = displayName;

        ChildNodes.AddRange(childNodes.ToList());        
    }
}
