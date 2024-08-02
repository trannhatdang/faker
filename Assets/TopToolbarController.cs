using UnityEngine;
using UnityEngine.UIElements;

public class TopToolbarController : MonoBehaviour
{
    VisualElement root;
    Label Money;
    Label Rating;
    [SerializeField] GameState gameState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        Money = root.Q<Label>("Money");
        Rating = root.Q<Label>("Rating");
    }

    // Update is called once per frame
    void Update()
    {
        Money.text = "Tiền: " + gameState.Money;
        Rating.text = "Rating : " + gameState.Rating;
    }
}
