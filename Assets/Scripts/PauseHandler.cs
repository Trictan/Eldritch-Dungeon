using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public static bool paused;
    public Canvas canvas;
    public SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setVisible(false);
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
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        setVisible(false);
        paused=false;
    }

}
