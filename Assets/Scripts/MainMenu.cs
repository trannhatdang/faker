using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public VisualElement ui;

    public Button playButton;
    public Button continueButton;
    public Button optionsButton;
    public Button quitButton;
    // Start is called before the first frame update
    void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    void OnEnable()
    {
        playButton = ui.Q<Button>("Play");
        playButton.clicked += OnPlayButtonCliked;
        continueButton = ui.Q<Button>("Continue");
        optionsButton = ui.Q<Button>("Options");
        quitButton = ui.Q<Button>("Exit");
    }

    void OnPlayButtonCliked()
    {
        GameManager.manager.LoadScene(1);
    }

    
}
