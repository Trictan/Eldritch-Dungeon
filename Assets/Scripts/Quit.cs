using UnityEngine;

public class Quit : monobehaviour
{
    public void quit_app()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false; // if editor
    }
}
