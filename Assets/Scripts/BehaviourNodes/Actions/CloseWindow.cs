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
        UnityEngine.AI.NavMeshAgent agent = npc.agent;
        Animator animator = npc.animator;
        Rigidbody2D rb = npc.rb;
        GameObject WindowToClose = null;

        if (GameManager.manager == null || npc == null)
        {
            StatusReason = "GameManager and/or NPC is null";
            return NodeStatus.Failure;
        }

        if(EvaluationCount == 0) 
        {
            WindowToClose = npc.WindowToClose;

            if(WindowToClose == null) return NodeStatus.Failure;
            agent.destination = WindowToClose.transform.position;
        }

        rb.MovePosition(rb.position + new Vector2(agent.desiredVelocity.x, agent.desiredVelocity.y).normalized * npc.getNPCData().walkSpeed * Time.fixedDeltaTime);

        animator.SetFloat("Speed", agent.desiredVelocity.sqrMagnitude);        
        animator.SetFloat("Vertical", agent.desiredVelocity.y);
        animator.SetFloat("Horizontal", agent.desiredVelocity.x);   

        if(agent.desiredVelocity.x >= 1f || agent.desiredVelocity.x <= -1f || agent.desiredVelocity.y >= 1f || agent.desiredVelocity.y <= -1f)
        {
            animator.SetFloat("LastVert", agent.desiredVelocity.y);
            animator.SetFloat("LastHort", agent.desiredVelocity.x);   
        }

        if(agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid) return NodeStatus.Failure;

        if(agent.desiredVelocity == Vector3.zero) 
        {
            animator.SetFloat("LastVert", 1);    
            animator.SetFloat("LastHort", 0); 
            WindowToClose.GetComponent<Window>().changeWindow(false);

            return NodeStatus.Success;
        }
        
        return NodeStatus.Running;
    }
    protected override void OnReset()
    {
        EvaluationCount = 0;
    }
}