using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public class SlackOff : Node
{
    NPC npc;
    Vector3 newPos = Vector3.zero;
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
            float xPos = Random.Range(npc.getMapData().leftBorder, npc.getMapData().rightBorder);
            float yPos = Random.Range(npc.getMapData().downBorder, npc.getMapData().upBorder);
            newPos = new UnityEngine.Vector3(xPos, yPos, 0);
        }

        npc.transform.position = Vector3.Lerp(npc.transform.position, newPos, Time.deltaTime * .1f);

        return NodeStatus.Running;
    }
    protected override void OnReset() {}
}