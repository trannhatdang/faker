using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public class IsThereAnOpenWindow : Condition
{
    private float m_DistanceToCheck;
    Pedestrians pedestrians;
    public IsThereAnOpenWindow(float maxDistance, Pedestrians pEdestrians) : base($"Are Windows within {maxDistance}f?")
    {
        m_DistanceToCheck = maxDistance;
        pedestrians = pEdestrians;
    }
    protected override void OnReset() { }
    protected override NodeStatus OnRun()
    {
        if (GameManager.manager == null || pedestrians == null)
        {
            StatusReason = "GameManager and/or NPC is null";
            return NodeStatus.Failure;
        }

        GameObject Window = pedestrians.ScanWindows(m_DistanceToCheck);

        if(Window != null && !pedestrians.isSmacked)
        {
            StatusReason = "yay window";
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }
}
