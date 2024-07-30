using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public class IsGoingToCloseWindow : Condition
{
    private float m_DistanceToCheck;
    NPC npc;
    public IsGoingToCloseWindow(float maxDistance, NPC Npc) : base($"Are Windows within {maxDistance}f?")
    {
        m_DistanceToCheck = maxDistance;
        npc = Npc;
    }

    protected override void OnReset() { }

    protected override NodeStatus OnRun()
    {
        if (GameManager.manager == null || npc == null)
        {
            StatusReason = "GameManager and/or NPC is null";
            return NodeStatus.Failure;
        }

        GameObject Window = npc.IsGoingToCloseWindow(m_DistanceToCheck);

        if(Window == null)
        {
            StatusReason = "NPC Not Going To Close lol";
            return NodeStatus.Failure;
        }

        return NodeStatus.Success;


    }
}
