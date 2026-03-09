using UnityEngine;

public class Quit : MonoBehaviour
{
    public void quit_app()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false; // if editor
    }
}
