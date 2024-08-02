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
        UnityEngine.AI.NavMeshAgent agent = npc.agent;
        Animator animator = npc.animator;
        Rigidbody2D rb = npc.rb;

        if (GameManager.manager == null || npc == null)
        {
            StatusReason = "GameManager and/or NPC is null";
            return NodeStatus.Failure;
        }

        if(EvaluationCount == 0 || Vector3.Distance(npc.transform.position, newPos) < 1f)
        {
            float xPos = Random.Range(npc.getMapData().leftBorder, npc.getMapData().rightBorder);
            float yPos = Random.Range(npc.getMapData().downBorder, npc.getMapData().upBorder);
            newPos = new Vector3(xPos, yPos, 0);
        }

        rb.MovePosition(rb.position + new Vector2(agent.desiredVelocity.x, agent.desiredVelocity.y).normalized * npc.getNPCData().walkSpeed / 100 * Time.fixedDeltaTime);

        animator.SetFloat("Speed", agent.desiredVelocity.sqrMagnitude);        
        animator.SetFloat("Vertical", agent.desiredVelocity.y);
        animator.SetFloat("Horizontal", agent.desiredVelocity.x);   

        if(agent.desiredVelocity.x >= 1f || agent.desiredVelocity.x <= -1f || agent.desiredVelocity.y >= 1f || agent.desiredVelocity.y <= -1f)
        {
            animator.SetFloat("LastVert", agent.desiredVelocity.y);
            animator.SetFloat("LastHort", agent.desiredVelocity.x);   
        }

        if(rb.linearVelocityX >= 1f || rb.linearVelocityX <= -1f || rb.linearVelocityY >= 1f || rb.linearVelocityY <= -1f)
        {
            animator.SetFloat("LastVert", rb.linearVelocityY);
            animator.SetFloat("LastHort", rb.linearVelocityX);   
        }

        if(npc.isSmacked) return NodeStatus.Failure;

        return NodeStatus.Running;
    }
    protected override void OnReset() {}
}