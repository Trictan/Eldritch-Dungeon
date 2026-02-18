using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_switch : MonoBehaviour
{
    public void scene_change(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
}
