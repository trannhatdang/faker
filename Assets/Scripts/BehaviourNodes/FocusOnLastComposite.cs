using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public class FocusOnLastComposite : CompositeNode
{
    public FocusOnLastComposite(string displayName, params Node[] childNodes) : base(displayName, childNodes) { }
    protected override NodeStatus OnRun()
    {
        if (CurrentChildIndex == ChildNodes.Count - 1)
        {
            NodeStatus lastNodeStatus = (ChildNodes[CurrentChildIndex] as Node).Run();
            return lastNodeStatus;
        }
        //Check the status of the last child
        NodeStatus childNodeStatus = (ChildNodes[CurrentChildIndex] as Node).Run();

        switch (childNodeStatus)
        {
            //Child failed - continue
            case NodeStatus.Failure:
            case NodeStatus.Success:
                CurrentChildIndex++;
                break;
            case NodeStatus.Running:
                break;
        }
        
        //The child succeeded somehow.
        return childNodeStatus == NodeStatus.Failure ? OnRun() : NodeStatus.Running;
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
