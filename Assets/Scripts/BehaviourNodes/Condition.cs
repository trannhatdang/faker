using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public abstract class Condition : Node
{
    public Condition(string name)
    {
        Name = name;
    }
}
