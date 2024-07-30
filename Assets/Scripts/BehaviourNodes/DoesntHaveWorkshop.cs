using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public class DoesntHaveWorkshop : Condition
{
    NPC npc;
    public DoesntHaveWorkshop(NPC Npc) : base($"Do they have a Workshop?")
    {
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

        return npc.getWorkSpot() ? NodeStatus.Failure : NodeStatus.Success;
    }
}
