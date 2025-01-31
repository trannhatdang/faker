using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public class SelectorComposite : CompositeNode
{
    public SelectorComposite(string displayName, params Node[] childNodes) : base(displayName, childNodes) { }
    protected override NodeStatus OnRun()
    {
        //We've reached the end of the ChildNodes and no one was successful
        if (CurrentChildIndex >= ChildNodes.Count)
        {
            return NodeStatus.Failure;
        }

        //Call the current child
        NodeStatus nodeStatus = (ChildNodes[CurrentChildIndex] as Node).Run();

        //Check the child's status - failure means try a new child, Success means done.
        if(nodeStatus == NodeStatus.Failure) CurrentChildIndex++;
        else if (nodeStatus == NodeStatus.Success) return NodeStatus.Success;   

        //If this point as been hit - then the current child is still running
        return NodeStatus.Running;
    }

    protected override void OnReset()
    {
        CurrentChildIndex = 0;

        for (int i = 0; i < ChildNodes.Count; i++)
        {
            (ChildNodes[i] as Node).Reset();
        }
    }
}
