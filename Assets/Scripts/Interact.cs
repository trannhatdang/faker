using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Interact : MonoBehaviour
{
    [SerializeField] Transform grabPoint;
    [SerializeField] Transform rayPoint;
    [SerializeField] float rayDistance;
    LineRenderer lineRenderer;
    Movement movement;
    GameObject grabbedObject;
    GameObject connectingObject;
    RaycastHit2D hitinfo;
    ProgressBar prog_bar;
    bool isConnecting = false;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        movement = GetComponent<Movement>();

        lineRenderer.positionCount = 0;
    }
    // Update is called once per frame
    void Update()
    {        
        hitinfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);        
        
        if(movement.LastHort == 1f)
        {
            rayPoint.transform.localPosition = new Vector2(0.3f, 0);
            grabPoint.transform.localPosition = new Vector2(0.6f, 0);
            hitinfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);
            Debug.DrawRay(rayPoint.position, transform.right * rayDistance);
        }

        if(movement.LastVert == 1f)
        {
            rayPoint.transform.localPosition = new Vector2(0, 0.3f);
            grabPoint.transform.localPosition = new Vector2(0, 0.7f);
            hitinfo = Physics2D.Raycast(rayPoint.position, transform.up, rayDistance);
            Debug.DrawRay(rayPoint.position, transform.up * rayDistance);
        }

        if(movement.LastHort == -1f)
        {
            rayPoint.transform.localPosition = new Vector2(-0.3f, 0);
            grabPoint.transform.localPosition = new Vector2(-0.6f, 0);
            hitinfo = Physics2D.Raycast(rayPoint.position, -transform.right, rayDistance);
            Debug.DrawRay(rayPoint.position, -transform.right * rayDistance);
        }

        if(movement.LastVert == -1f)
        {
            rayPoint.transform.localPosition = new Vector2(0, -0.5f);
            grabPoint.transform.localPosition = new Vector2(0, -0.7f);
            hitinfo = Physics2D.Raycast(rayPoint.position, -transform.up, rayDistance);
            Debug.DrawRay(rayPoint.position, -transform.up * rayDistance);
        }

        if(hitinfo.collider != null)
        {             
            checkWork();
            checkSmack();
            checkStorage();
        }
        
        checkPickUp();
        checkConnect();
    }
    void checkPickUp()
    {    
        // VisualElement PickUpButton = GameManager.manager.UI.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("SecondButton"); 

        // if(hitinfo.collider.gameObject.CompareTag("ItemMovable") || hitinfo.collider.gameObject.CompareTag("Workshop"))
        // {
        //     PickUpButton.style.display = DisplayStyle.Flex;
        // }
                   
        if(Input.GetKeyDown(KeyCode.G) && grabbedObject == null && hitinfo.collider != null && 
            (hitinfo.collider.gameObject.CompareTag("ItemMovable") || hitinfo.collider.gameObject.CompareTag("Workshop")))
        {            
            PickUp(hitinfo.collider.gameObject);
            return;             
        }

        if(Input.GetKeyDown(KeyCode.G) && grabbedObject != null)
        {
            grabbedObject.transform.parent = null;
            grabbedObject.transform.position = grabPoint.position;
            grabbedObject = null;
        }
    }
    void checkConnect()
    {
        if(!isConnecting)
        {
            if(hitinfo.collider == null) return;
            
            if(Input.GetKeyDown(KeyCode.C) && (hitinfo.collider.gameObject.CompareTag("Workshop") || hitinfo.collider.gameObject.CompareTag("NPC")))
            {
                connectingObject = hitinfo.collider.gameObject;
                isConnecting = true;               

                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, hitinfo.collider.gameObject.transform.position);
                lineRenderer.SetPosition(1, transform.position);
            }

            return;
        }
        
        lineRenderer.material.mainTextureScale = new Vector2(Vector2.Distance(transform.position, connectingObject.transform.position) / lineRenderer.startWidth, 1.0f);
        lineRenderer.SetPosition(1, transform.position);          

        if(Input.GetKeyDown(KeyCode.C))
        {
            isConnecting = false;
            lineRenderer.positionCount = 0;
            if(hitinfo.collider.gameObject.CompareTag("Workshop") || hitinfo.collider.gameObject.CompareTag("NPC"))
            {                    
                if(connectingObject.CompareTag("NPC"))
                {
                    NPC npc = connectingObject.GetComponent<NPC>();
                    npc.setWorkSpot(hitinfo.collider.gameObject.GetComponent<Workshop>().WorkSpot);
                }
                else
                {   
                    hitinfo.collider.gameObject.GetComponent<NPC>().setWorkSpot(connectingObject.GetComponent<Workshop>().WorkSpot);
                }
            } 
            else if(connectingObject.CompareTag("NPC"))
            {                
                {
                    connectingObject.GetComponent<NPC>().setWorkSpot(null);
                }
            }            
            connectingObject = null;
        }              
    }
    void checkWork()
    {
        VisualElement WorkButton = GameManager.manager.UI.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("FirstButton");
        if(hitinfo.collider.gameObject.CompareTag("Workshop"))
        {
            WorkButton.style.display = DisplayStyle.Flex;
            if(Input.GetKeyDown(KeyCode.Z) && hitinfo.collider.gameObject.CompareTag("Workshop"))
            {          
                prog_bar = hitinfo.collider.gameObject.GetComponent<UIDocument>().rootVisualElement.Q<ProgressBar>("ProgressBar");
                if(prog_bar.value < 100) prog_bar.value += 1;
                else prog_bar.value = 0;

                if(prog_bar.value >= 100) 
                {
                    GameManager.manager.ChangeMoney(20000);
                    prog_bar.value = 0;
                }
            }
        }
        else WorkButton.style.display = DisplayStyle.None;
        
    }
    void checkSmack()
    {
        VisualElement SmackButton = GameManager.manager.UI.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("ThirdButton");
        if(hitinfo.collider.gameObject.CompareTag("NPC"))
        {
            SmackButton.style.display = DisplayStyle.Flex;
            if(Input.GetKeyDown(KeyCode.R) && hitinfo.collider.gameObject.CompareTag("NPC"))
            {
                NPC npc = hitinfo.collider.gameObject.GetComponent<NPC>();
                npc.setSmack(true);
            }
        } else SmackButton.style.display = DisplayStyle.None;
        
    }
    void checkStorage()
    {
        if(Input.GetKeyDown(KeyCode.R) && hitinfo.collider.gameObject.CompareTag("Storage"))
        {
            GameManager.manager.OpenInventory();
        }
    }
    public void PickUp(GameObject gameObject)
    {
        grabbedObject = gameObject;
        grabbedObject.transform.position = grabPoint.position;
        grabbedObject.transform.parent = transform;
    }
}
