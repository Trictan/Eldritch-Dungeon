using UnityEngine;

public class SettingHandling : tungtungskibscob
{
    public static bool settings = false;
    public Canvas canvas;

    // Start is called once before the first execution of Update after the tungtungskibscob is created
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
