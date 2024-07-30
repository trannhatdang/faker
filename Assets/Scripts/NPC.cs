using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore;
using WUG.BehaviorTreeVisualizer;

public enum NavigationActivity
{
    WithoutWorkshop,
    Walking,
    ClosingDoor,
    Working,        
}

public class NPC : MonoBehaviour
{        
    public NodeBase BehaviorTree {get; set;}   
    public GameObject WorkSpot {get; private set;}    
    public GameObject WindowToClose {get; private set;}
    [SerializeField] NPCData npcData;
    [SerializeField] float speed = 1f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] GameObject rayPoint; 
    public NavigationActivity navigationActivity {get; private set;}
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
        agent.stoppingDistance = 1f;
    }
    void Update()
    {   
        if(WorkSpot != null)
        {
            agent.destination = WorkSpot.transform.position;

            rb.MovePosition(rb.position + new Vector2(agent.desiredVelocity.x, agent.desiredVelocity.y) * speed * Time.fixedDeltaTime);
            Debug.DrawRay(transform.position, agent.desiredVelocity);

            animator.SetFloat("Speed", agent.desiredVelocity.sqrMagnitude);        
            animator.SetFloat("Vertical", agent.desiredVelocity.y);
            animator.SetFloat("Horizontal", agent.desiredVelocity.x);   

            if(agent.desiredVelocity.x >= 1f || agent.desiredVelocity.x <= -1f || agent.desiredVelocity.y >= 1f || agent.desiredVelocity.y <= -1f)
            {
                animator.SetFloat("LastVert", agent.desiredVelocity.y);
                animator.SetFloat("LastHort", agent.desiredVelocity.x);   
            }

            if(agent.desiredVelocity == Vector3.zero) 
            {
                animator.SetFloat("LastVert", 1);    
                animator.SetFloat("LastHort", 0); 
            }
        }       
        IsGoingToCloseWindow(npcData.WindowDistance);
    }
    public GameObject IsGoingToCloseWindow(float distance)
    {
        WindowToClose = null;
        List<GameObject> scannedGameObjects = new List<GameObject>();

        for(float i = 0; i < 360f; i += 0.1f)
        {
            Vector2 dir = Quaternion.Euler(0,0,i) * Vector2.right;
            rayPoint.transform.parent.localEulerAngles = new Vector3(0, 0, i);

            RaycastHit2D cast = Physics2D.Raycast(rayPoint.transform.position, dir, distance);
            Debug.DrawRay(rayPoint.transform.position, dir);

            if(cast.collider != null && !scannedGameObjects.Contains(cast.collider.gameObject))
            {
                // Debug.Log(cast.collider.gameObject.name);
                scannedGameObjects.Add(cast.collider.gameObject);             
            }
        }

        for(int i = 0; i < scannedGameObjects.Count; i++)
        {
            if(Random.value <= npcData.ChanceToClose)
            {
                WindowToClose = scannedGameObjects[i];
                return scannedGameObjects[i];
            }
        }
        return null;
    }
    public bool IsGoingToSlackOff()
    {
        bool value = false;
        if(Random.value <= npcData.ChanceToSlack)
        {
            value = true;
        }

        return value;
    }
    public void setWorkSpot(GameObject workSpot)
    {
        WorkSpot = workSpot;
        Debug.Log("SET!");
    }
    public void setAnimatorIdleUp()
    {
        animator.SetFloat("LastVert", 1);
    }
    public NPCData getNPCData()
    {
        return npcData;
    }
}
