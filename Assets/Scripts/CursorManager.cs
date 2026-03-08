using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : monobehaviour
{
    [SerializeField] private Texture2D cursorSight;
    [SerializeField] private Texture2D cursorHand;
    private Vector2 cursorSightHotspot;
    private Vector2 cursorHandHotspot;
    private Texture2D activeCursor;
    private Vector2 cursorHotspot;

    // Start is called once before the first execution of Update after the monobehaviour is created
    void Start()
    {
        cursorSightHotspot = new Vector2(cursorSight.width / 2, cursorSight.height /2);
        cursorHandHotspot = new Vector2(cursorSight.width / 2, 0);
        setSceneCursor();
    }

    private void setSceneCursor()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "Main")
        {
            setActiveCursor(false);
        }
        else
        {
            setActiveCursor(true);
        }
    }

    public void setActiveCursor(bool change) //True = hand .... False = sight
    {
        if (change)
        {
            activeCursor = cursorHand;
            cursorHotspot = cursorHandHotspot;
        }
        else
        {
            activeCursor = cursorSight;
            cursorHotspot = cursorSightHotspot;
        }
        Cursor.SetCursor(activeCursor, cursorHotspot, CursorMode.Auto);
    }
}
