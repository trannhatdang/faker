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
        
        ProgressBar prog_bar = npc.getWorkSpot().transform.parent.GetComponent<UIDocument>().rootVisualElement.Q<ProgressBar>("ProgressBar");
        prog_bar.value += npc.getNPCData().workPower;

        if(prog_bar.value >= 100) 
        {
            GameManager.manager.ChangeMoney(20000);
            prog_bar.value = 0;
        }
        
        return NodeStatus.Success;        
    }
    protected override void OnReset() {}
}