using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public class SlackOff : Node
{
    NPC npc;
    Vector3 newPos = new Vector3.zero;
    public SlackOff(NPC Npc)
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

        if(EvaluationCount == 0 || UnityEngine.Vector3.Distance(npc.transform.position, newPos) < 1f)
        {
            float xPos = Random.Range(npc.mapData.leftBorder, npc.mapData.rightBorder);
            float yPos = Random.Range(npc.mapData.downBorder, npc.mapData.upBorder);
            newPos = new UnityEngine.Vector3(xPos, yPos, 0);
        }

        npc.transform.DOMove(newPos);
        
        if(WorkSpot != null) return NodeStatus.Success;

        return NodeStatus.Running;
    }
}