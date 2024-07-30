using UnityEngine;
using UnityEngine.UIElements;
using WUG.BehaviorTreeVisualizer;

public class Work : Node
{
    NPC npc;
    public Work(NPC Npc)
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

        if(npc.agent.desiredVelocity == Vector3.zero) 
        {
            ProgressBar prog_bar = npc.getWorkSpot().transform.parent.GetComponent<UIDocument>().rootVisualElement.Q<ProgressBar>("ProgressBar");
            prog_bar.value += npc.getNPCData().workPower;
            if(prog_bar.value == 100) 
            {
                prog_bar.value = 0;
                return NodeStatus.Success;
            }
        }

        return NodeStatus.Running;
    }
    protected override void OnReset() {}
}