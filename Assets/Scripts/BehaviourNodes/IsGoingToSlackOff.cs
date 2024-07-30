using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public class IsGoingToSlackoff : Condition
{
    NPC npc;
    public IsGoingToSlackoff(NPC Npc) : base($"Are They going to Slack Off?")
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

        bool value = npc.IsGoingToSlackOff();

        if(!value)
        {
            StatusReason = "NPC Not Going To Close lol";
            return NodeStatus.Failure;
        }

        return NodeStatus.Success;
    }
}
