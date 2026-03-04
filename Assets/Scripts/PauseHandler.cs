using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public static bool paused;
    public Canvas canvas;
    public SpriteRenderer spriteRenderer;

    private GameObject cursor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setVisible(false);
        cursor = GameObject.FindGameObjectWithTag("cursor");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }
    }

    void setVisible(bool val)
    {
        canvas.enabled = val;
        spriteRenderer.enabled = val;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        setVisible(true);
        paused=true;
        cursor.GetComponent<CursorManager>().setActiveCursor(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        setVisible(false);
        paused=false;
        cursor.GetComponent<CursorManager>().setActiveCursor(false);
    }

}
