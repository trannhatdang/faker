using UnityEngine;
using UnityEngine.UIElements;
using WUG.BehaviorTreeVisualizer;

public class CloseWindow : Node
{
    NPC npc;
    public CloseWindow(NPC Npc)
    {
        npc = Npc;
    }
    protected override NodeStatus OnRun()
    {
        if (GameManager.manager == null || npc == null)
        {
            StatusReason = "GameManager and/or NPC is null";
            return NodeStatus.Failure;
        }
        
        

        return NodeStatus.Running;
    }
    protected override void OnReset()
    {
        EvaluationCount = 0;
    }
}