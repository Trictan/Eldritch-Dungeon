using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_switch : MonoBehaviour
{
    public void ChangeToScene(string scene_name)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene_name);
    }

    public void QuitApp()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false; // if editor
    }

    public void ExitToMenu()
    {
        PauseHandler.paused=false;
        ChangeToScene("Menu");
    }

    //----Don't touch!!!---------
    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
