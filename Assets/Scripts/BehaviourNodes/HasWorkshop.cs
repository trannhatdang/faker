using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public class HasWorkshop : Condition
{
    NPC npc;
    public HasWorkshop(NPC Npc) : base($"Do they have a Workshop?")
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

        return npc.WorkSpot ? NodeStatus.Success : NodeStatus.Failure;
    }
}
