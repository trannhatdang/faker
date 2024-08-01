using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager manager {get; private set;}
    public event EventHandler OnChangeInventory;
    public float LoadingValue {get; private set;}
    public static InventoryUIController inventoryUIController {get; private set;}
    List<GameObject> AllNPCs;
    GameObject door;   
    void Awake()
    {
        if(manager != null && manager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            manager = this;
        }

        DontDestroyOnLoad(this);

        AllNPCs = Resources.LoadAll<GameObject>("NPCs").ToList();
        if(GameObject.FindWithTag("UI").GetComponent<InventoryUIController>() != null) inventoryUIController = GameObject.FindWithTag("UI").GetComponent<InventoryUIController>();
    }
    void Start()
    {
        door = GameObject.FindGameObjectWithTag("Door");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public IEnumerator Hire(string name)
    // {
    //     var toHire = AllNPCs.Find(x => x.name == name);
    //     float timer = 5f;

    //     for(; timer >= 0; timer -= Time.deltaTime) 
    //     {            
    //         yield return null;
    //     }        
        
    //     Instantiate(toHire, door.transform.position, Quaternion.identity);
        
    // }

    public void Hire(string name)
    {
        GameObject toHire = AllNPCs.Find(x => x.name == name);

        Instantiate(toHire, door.transform.position, Quaternion.identity);
    }

    public void LoadScene(int ID)
    {
        StartCoroutine(LoadSceneAsync(ID));
    }

    IEnumerator LoadSceneAsync(int ID)
    {
        AsyncOperation LoadScene = SceneManager.LoadSceneAsync(ID);
        UI ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();

        ui.setUIActive("LoadingScreen",true);

        while(!LoadScene.isDone)
        {
            LoadingValue = Mathf.Clamp(LoadScene.progress * 100, 0, 100);

            yield return null;
        }
    }
    public void ChangeInventory()
    {        
        OnChangeInventory?.Invoke(this, EventArgs.Empty);
    }
}
