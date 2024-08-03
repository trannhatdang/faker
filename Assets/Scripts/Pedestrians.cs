using UnityEngine;
using System.Collections;
using WUG.BehaviorTreeVisualizer;
using System.Collections.Generic;
using UnityEngine.AI;

public class Pedestrians : MonoBehaviour, IBehaviorTree
{
    GameObject Window;
    float timer;
    private Coroutine m_BehaviorTreeRoutine;
    public NodeBase BehaviorTree {get; set;}    
    public NavMeshAgent agent {get; private set;}
    public Rigidbody2D rb {get; private set;}
    public Animator animator {get; private set;}
    public bool isSmacked {get; private set;}
    [SerializeField] GameObject rayPoint;
    [SerializeField] NPCData npcData;
    void Start()
    {
        timer = npcData.smackTimer;

        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

		agent.updateRotation = false;
		agent.updateUpAxis = false;
        agent.stoppingDistance = 1f;

        GenerateBehaviorTree();
    
        if (m_BehaviorTreeRoutine == null && BehaviorTree != null)
        {
            m_BehaviorTreeRoutine = StartCoroutine(RunBehaviorTree());
        }
    }
    void FixedUpdate()
    {
        if(isSmacked) 
        {
            timer -= Time.fixedDeltaTime;
            Debug.Log("SmackTimer con " + timer);
        }
        
        if(timer <= 0) 
        {
            isSmacked = false;
            timer = npcData.smackTimer;
        }
    }
    private void GenerateBehaviorTree()
    {
        BehaviorTree = 
            new SelectorComposite("Pedestrians",
                // new SequenceComposite("Is there a Window?",
                //     new IsThereAnOpenWindow(1f, this),
                //     new LookingAtWindow(this)),
                new WalkingAround(this));
    }
    private IEnumerator RunBehaviorTree()
    {
        while (enabled)
        {
            if (BehaviorTree == null)
            {
                continue;
            }

            (BehaviorTree as Node).Run();

            yield return null;
        }
    }
    private void OnDestroy()
    {
        if (m_BehaviorTreeRoutine != null)
        {
            StopCoroutine(m_BehaviorTreeRoutine);
        }
    }
    public GameObject ScanWindows(float distance)
    {
        List<GameObject> scannedGameObjects = new List<GameObject>();

        for(float i = 0; i < 360f; i += 0.1f)
        {
            Vector2 dir = Quaternion.Euler(0,0,i) * Vector2.right;
            rayPoint.transform.parent.localEulerAngles = new Vector3(0, 0, i);

            RaycastHit2D cast = Physics2D.Raycast(rayPoint.transform.position, dir, distance);

            if(cast.collider != null && !scannedGameObjects.Contains(cast.collider.gameObject))
            {
                scannedGameObjects.Add(cast.collider.gameObject);             
            }
        }
        
        for(int i = 0; i < scannedGameObjects.Count; i++)
        {
            if(scannedGameObjects[i].CompareTag("Window")) 
            {
                Window = scannedGameObjects[i];
                return scannedGameObjects[i];
            }
        }
        return null;
    }

    public GameObject getWindow()
    {
        return Window;
    }
    public NPCData getNPCData() {return npcData;}    
    public void setSmack(bool value) {isSmacked = value;}
}
    