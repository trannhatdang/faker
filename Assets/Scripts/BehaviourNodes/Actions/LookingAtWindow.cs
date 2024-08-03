using UnityEngine;
using UnityEngine.UIElements;
using WUG.BehaviorTreeVisualizer;

public class LookingAtWindow : Node
{
    Pedestrians pedestrians;
    public LookingAtWindow(Pedestrians pEdestrians)
    {
        pedestrians = pEdestrians;
    }
    protected override NodeStatus OnRun()
    {
        UnityEngine.AI.NavMeshAgent agent = pedestrians.agent;
        Animator animator = pedestrians.animator;
        Rigidbody2D rb = pedestrians.rb;
        GameObject LastWindow = null;
        if (GameManager.manager == null || pedestrians == null)
        {
            StatusReason = "GameManager and/or pedestrians is null";
            return NodeStatus.Failure;
        }
        
        if(LastWindow == null) LastWindow = pedestrians.getWindow();

        if(EvaluationCount == 0 || LastWindow != pedestrians.getWindow()) 
        {
            LastWindow = pedestrians.getWindow();
            agent.destination = LastWindow.transform.position;
        }

        rb.MovePosition(rb.position + new Vector2(agent.desiredVelocity.x, agent.desiredVelocity.y).normalized * pedestrians.getNPCData().walkSpeed * Time.fixedDeltaTime);

        animator.SetFloat("Speed", agent.desiredVelocity.sqrMagnitude);        
        animator.SetFloat("Vertical", agent.desiredVelocity.y);
        animator.SetFloat("Horizontal", agent.desiredVelocity.x);   

        if(agent.desiredVelocity.x >= 1f || agent.desiredVelocity.x <= -1f || agent.desiredVelocity.y >= 1f || agent.desiredVelocity.y <= -1f)
        {
            animator.SetFloat("LastVert", agent.desiredVelocity.y);
            animator.SetFloat("LastHort", agent.desiredVelocity.x);   
        }

        if(agent.desiredVelocity == Vector3.zero && !pedestrians.isSmacked)
        {
            GameManager.manager.ChangeRating(-1);
        } 

        if(agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid || pedestrians.isSmacked) return NodeStatus.Failure;

        return NodeStatus.Success;
    }
    protected override void OnReset() {}
}