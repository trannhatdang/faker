using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public abstract class Node : NodeBase
{
    public int EvaluationCount;
    public virtual NodeStatus Run()
    {
        NodeStatus nodeStatus = OnRun();

        EvaluationCount++;

        if (nodeStatus != NodeStatus.Running)
        {
            Reset();
        }

        return nodeStatus;
    }

    public void Reset()
    {
        EvaluationCount = 0;
        OnReset();
    }

    protected abstract NodeStatus OnRun();
    protected abstract void OnReset();
}
