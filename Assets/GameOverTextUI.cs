using UnityEngine;
using TMPro;

public class GameOverTextUI : MonoBehaviour
{
    public TextMeshProUGUI textElement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textElement.text = "You traversed " + GameController.traversedRooms+" rooms on floor "+ GameController.floor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
