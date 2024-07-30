using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadingMenu", menuName = "Scriptable Objects/LoadingMenu")]
public class LoadingMenu : ScriptableObject
{
    public float prog = 0;

    void Update()
    {
        prog = GameManager.manager.LoadingValue;
    }
}
