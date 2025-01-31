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

public class NPC : MonoBehaviour, IBehaviorTree
{        
    public GameObject WindowToClose {get; private set;}
    public NavigationActivity navigationActivity {get; private set;}
    public NavMeshAgent agent {get; private set;}
    public Rigidbody2D rb {get; private set;}
    public Animator animator {get; private set;}
    public NodeBase BehaviorTree {get; set;}
    public bool isSmacked {get; private set;}
    private float smackTimer = 0;
    [SerializeField] GameObject WorkSpot;    
    [SerializeField] NPCData npcData;
    [SerializeField] GameObject rayPoint;
    private Coroutine m_BehaviorTreeRoutine;
    private void GenerateBehaviorTree()
    {
        BehaviorTree = 
            new FocusOnLastComposite("Control NPC",
                new SequenceComposite("Do they have a workshop?",
                    new DoesntHaveWorkshop(this),
                    new NotHavingWorkshop(this)),
                new SequenceComposite("Are they going to slack off?",
                    new IsGoingToSlackoff(this),
                    new SlackOff(this)),
                new SequenceComposite("Work Sequence",
                    new GoingToWork(this),
                    new Work(this)));
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
    void Start()
    {
        isSmacked = false;
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
    void Update()
    {            
        if(isSmacked) 
        {
            smackTimer -= Time.deltaTime;
            Debug.Log("SmackTimer con " + smackTimer);
        }
        
        if(smackTimer <= 0) 
        {
            isSmacked = false;
            smackTimer = npcData.smackTimer;
        }
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

            if(cast.collider != null && !scannedGameObjects.Contains(cast.collider.gameObject))
            {
                scannedGameObjects.Add(cast.collider.gameObject);             
            }
        }
        
        for(int i = 0; i < scannedGameObjects.Count; i++)
        {
            if(Random.value <= npcData.ChanceToClose && scannedGameObjects[i].CompareTag("Window"))
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
        int randomval = Random.Range(0, 1000); 
        if(randomval <= npcData.ChanceToSlack)
        {
            // Debug.Log("Gia tri slack la" + randomval);
            value = true;
        }
        return value;
    }
    public void setWorkSpot(GameObject workSpot)
    {
        WorkSpot = workSpot;
    }
    public NPCData getNPCData() {return npcData;}
    public GameObject getWorkSpot()
    {
        return WorkSpot;
    }
    public void setSmack(bool value) {isSmacked = value;}
}
