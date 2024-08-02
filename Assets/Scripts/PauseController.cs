using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseController : MonoBehaviour
{
    VisualElement root;
    VisualElement PauseMenu;
    Button resume;
    Button escape;
    Button save;
    bool isPaused = false;
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        PauseMenu = root.Q<VisualElement>("PauseMenu");
        resume = root.Q<Button>("ResumeButton");
        save = root.Q<Button>("SaveButton");
        escape = root.Q<Button>("EscapeButton");

        PauseMenu.style.display = DisplayStyle.None;
        GameManager.manager.OnPausePressed += OnPausePressed;
    }
    void Update()
    {

    }
    void OnPausePressed(object sender, EventArgs e)
    {
        if(!isPaused)
        {
            PauseMenu.style.display = DisplayStyle.Flex;
        }
        else
        {
            PauseMenu.style.display = DisplayStyle.None;
        }
    }
    void OnResumeClicked()
    {

    }
    void OnSaveClicked()
    {

    }
    void OnEscapeClicked()
    {

    }
}