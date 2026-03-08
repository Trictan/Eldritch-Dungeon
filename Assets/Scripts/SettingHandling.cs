using UnityEngine;

public class SettingHandling : monobehaviour
{
    public static bool settings = false;
    public Canvas canvas;

    // Start is called once before the first execution of Update after the monobehaviour is created
    void Start()
    {
        setVisible(settings);
    }

    public void changeActive()
    {
        settings = !settings;
        setVisible(settings);
    }

    private void setVisible(bool show)
    {
        canvas.enabled = show;
    }
}
