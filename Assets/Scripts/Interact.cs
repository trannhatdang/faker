using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Interact : MonoBehaviour
{
    [SerializeField] Transform grabPoint;
    [SerializeField] Transform rayPoint;
    [SerializeField] float rayDistance;
    Movement movement;
    GameObject grabbedObject;
    RaycastHit2D hitinfo;
    ProgressBar prog_bar;
    void Start()
    {
        movement = GetComponent<Movement>();
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
            grabPoint.transform.localPosition = new Vector2(0, 0.5f);
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
            grabPoint.transform.localPosition = new Vector2(0, -0.65f);
            hitinfo = Physics2D.Raycast(rayPoint.position, -transform.up, rayDistance);
            Debug.DrawRay(rayPoint.position, -transform.up * rayDistance);
        }

        checkPickUp(); 
        checkConnect();
        checkWork();
    }

    void checkPickUp()
    {
        if(hitinfo.collider != null)
        {
            if(Input.GetKeyDown(KeyCode.G) && grabbedObject == null && (hitinfo.collider.gameObject.CompareTag("ItemMovable") || 
            hitinfo.collider.gameObject.CompareTag("Workshop")))
            {
                grabbedObject = hitinfo.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.parent = transform;
                return;
            }
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
        if(hitinfo.collider != null && hitinfo.collider.gameObject.CompareTag("Workshop"))
        {
            if(Input.GetKeyDown(KeyCode.C)) 
            {
                grabbedObject = hitinfo.collider.gameObject;
            }
        }
    }

    void checkWork()
    {
        if(Input.GetKeyDown(KeyCode.Z) && hitinfo.collider != null && hitinfo.collider.gameObject.CompareTag("Workshop"))
        {          
            prog_bar = hitinfo.collider.gameObject.GetComponent<UIDocument>().rootVisualElement.Q<ProgressBar>("ProgressBar");
            if(prog_bar.value < 100) prog_bar.value += 20;
            else prog_bar.value = 0;    
        }
    }
}
