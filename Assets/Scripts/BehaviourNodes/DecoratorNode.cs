using JetBrains.Annotations;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public abstract class DecoratorNode : Node
{
    public DecoratorNode(string displayName, Node node)
    {
        Name = displayName;
        ChildNodes.Add(node);
    }
}
