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
    [SerializeField] GameObject WorkSpot;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] NPCData npcData;  
    [SerializeField] GameObject rayPoint; 
    public NavigationActivity navigationActivity {get; private set;}
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }
    void Update()
    {             
        // agent.destination = WorkSpot.transform.position;
        
        animator.SetFloat("Speed", agent.desiredVelocity.sqrMagnitude);        
        animator.SetFloat("Vertical", agent.desiredVelocity.y);
        animator.SetFloat("Horizontal", agent.desiredVelocity.x);   

        if(agent.desiredVelocity.x >= 1f || agent.desiredVelocity.x <= -1f || agent.desiredVelocity.y >= 1f || agent.desiredVelocity.y <= -1f)
        {
            animator.SetFloat("LastVert", agent.desiredVelocity.y);
            animator.SetFloat("LastHort", agent.desiredVelocity.x);   
        }

        IsGoingToCloseWindow(npcData.WindowDistance);
    }
    public GameObject IsGoingToCloseWindow(float distance)
    {
        List<GameObject> gameObjects = new List<GameObject>();

        for(float i = 0; i < 360f; i++)
        {
            Vector2 dir = Quaternion.Euler(0,0,i) * Vector2.right;
            rayPoint.transform.parent.localEulerAngles = new Vector3(0, 0, i);
            RaycastHit2D cast = Physics2D.Raycast(rayPoint.transform.position, dir, distance);
            Debug.DrawRay(rayPoint.transform.position, dir);

            if(cast.collider != null)
            {
                if(!gameObjects.Contains(cast.collider.gameObject)) 
                {
                    Debug.Log(cast.collider.gameObject.name);
                    gameObjects.Add(cast.collider.gameObject);
                }                
            }
        }

        for(int i = 0; i < gameObjects.Count; i++)
        {
            if(Random.value <= npcData.ChanceToClose)
            {
                return gameObjects[i];
            }
        }

        return null;
    }
}
