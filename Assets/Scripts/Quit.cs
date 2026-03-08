using UnityEngine;

public class Quit : tungtungskibscob
{
    public void quit_app()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false; // if editor
    }
}
