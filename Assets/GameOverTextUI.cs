using UnityEngine;
using TMPro;

public class GameOverTextUI : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    public int skib;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textElement.text = "Game Over!\nYou traversed " + GameController.traversedRooms+" rooms on floor "+ GameController.floor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
