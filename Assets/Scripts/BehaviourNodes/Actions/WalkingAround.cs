using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public class WalkingAround : Node
{
    Pedestrians pedestrians;
    Vector3 newPos = Vector3.zero;
    public WalkingAround(Pedestrians pEdestrians)
    {
        pedestrians = pEdestrians;
    }
    protected override NodeStatus OnRun()
    {
        UnityEngine.AI.NavMeshAgent agent = pedestrians.agent;
        Animator animator = pedestrians.animator;
        Rigidbody2D rb = pedestrians.rb;

        if (GameManager.manager == null || pedestrians == null)
        {
            StatusReason = "GameManager and/or NPC is null";
            return NodeStatus.Failure;
        }

        if(EvaluationCount == 0 || Vector3.Distance(pedestrians.transform.position, newPos) < 1f || agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            MapData mapData = GameManager.manager.getMapData();;
            float xPos = Random.Range(mapData.leftOutsideBorder, mapData.rightOutsideBorder);
            float yPos = Random.Range(mapData.downOutsideBorder, mapData.upOutsideBorder);
            newPos = new Vector3(xPos, yPos, 0);
        }

        rb.MovePosition(rb.position + new Vector2(agent.desiredVelocity.x, agent.desiredVelocity.y).normalized * pedestrians.getNPCData().walkSpeed / 100 * Time.fixedDeltaTime);

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



        return NodeStatus.Success;
    }
    protected override void OnReset() {}
}