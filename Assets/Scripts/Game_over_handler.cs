using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_over_handler : MonoBehaviour
{
    public GameObject player;
    public string scene_name;

    // Update is called once per frame
    void Update()
    {
        float health = player.GetComponent<LifeSystem>().GetCurrentHealth();
        if(health <= 0)
        {
            SceneManager.LoadScene(scene_name);
        }
    }
}
