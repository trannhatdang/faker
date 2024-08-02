using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    List<GameObject> AllNPCs;
    GameObject door;   
    public static GameManager manager {get; private set;}
    public float LoadingValue {get; private set;}
    public static InventoryUIController inventoryUIController {get; private set;}
    public GameState gameState {get; private set;}
    public event EventHandler OnChangeInventory;
    public event EventHandler OnPausePressed;
    public GameObject UI {get; private set;}
    [SerializeField] Inventory inventory;
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
        gameState = Resources.Load<GameState>("ScriptableObjects/GameState");
        
        if(GameObject.FindWithTag("UI").GetComponent<InventoryUIController>() != null) 
        {
            UI = GameObject.FindWithTag("UI");
            inventoryUIController = UI.GetComponent<InventoryUIController>();
        }
    }
    void Start()
    {
        door = GameObject.FindGameObjectWithTag("Door");
    }
    void FixedUpdate()
    {
        ChangeRating(-.1f);
        if(gameState.Rating == 0) 
        {
            gameState.Rating = 1000f;
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnPausePressed?.Invoke(this, EventArgs.Empty);
        }
    }
    public void Hire(string name)
    {
        GameObject toHire = AllNPCs.Find(x => x.name == name);

        Debug.Log(name);

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
    public void ChangeInventory(GameObject gameObject)
    {        
        inventory.items.Add(gameObject);
        OnChangeInventory?.Invoke(this, EventArgs.Empty);
    }
    public void OpenInventory()
    {
        inventoryUIController.OpenInventory();
    }
    public void ChangeMoney(int value)
    {
        gameState.Money += value;
    }
    public void ChangeRating(float value)
    {
        gameState.Rating += value;
    }
}
