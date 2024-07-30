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
    [SerializeField] GameObject WorkSpot;    
    [SerializeField] NPCData npcData;
    [SerializeField] MapData mapData;
    [SerializeField] GameObject rayPoint; 

    public NodeBase BehaviorTree { get; set; }
    private Coroutine m_BehaviorTreeRoutine;

    private void GenerateBehaviorTree()
    {
        BehaviorTree = 
            new SelectorComposite("Control NPC",
                new SequenceComposite("Do they have a workshop?",
                    new DoesntHaveWorkshop(this),
                    new SlackOff(this)),
                new SequenceComposite("Are they going to slack off?",
                    new IsGoingToSlackoff(this),
                    new SlackOff(this)),
                new SequenceComposite("Are they going to close a window?",
                    new IsGoingToCloseWindow(npcData.WindowDistance, this),
                    new CloseWindow(this)),
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

            yield return 0.1f;
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
    }
    public NPCData getNPCData()
    {
        return npcData;
    }
    public MapData getMapData()
    {
        return mapData;
    }
    public GameObject getWorkSpot()
    {
        return WorkSpot;
    }

}
