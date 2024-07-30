using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager {get; private set;}
    public float LoadingValue {get; private set;}
    List<GameObject> AllNPCs;
    GameObject door;
    [SerializeField] UI ui;        
    void Awake()
    {
        if(manager != null && manager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            manager = this;
        }

        DontDestroyOnLoad(this);

        ui = GameObject.FindWithTag("UI").GetComponent<UI>();
        AllNPCs = Resources.LoadAll<GameObject>("NPCs").ToList();
    }
    void Start()
    {
        door = GameObject.FindGameObjectWithTag("Door");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Hire(string name)
    {
        var toHire = AllNPCs.Find(x => x.name == name);
        float timer = 5f;

        for(; timer >= 0; timer -= Time.deltaTime) 
        {            
            yield return null;
        }        
        
        Instantiate(toHire, door.transform.position, Quaternion.identity);
        
    }

    public void LoadScene(int ID)
    {
        StartCoroutine(LoadSceneAsync(ID));
    }

    IEnumerator LoadSceneAsync(int ID)
    {
        AsyncOperation LoadScene = SceneManager.LoadSceneAsync(ID);

        ui.setUIActive("LoadingScreen",true);

        while(!LoadScene.isDone)
        {
            LoadingValue = Mathf.Clamp(LoadScene.progress * 100, 0, 100);

            yield return null;
        }
    }
}
