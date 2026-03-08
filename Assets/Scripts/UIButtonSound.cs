using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtonSound : monobehaviour{

    void Start()
    {
       AddSoundToButtons();
    }

     void OnEnable()
    {
        // Listen too when scene load are called
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Runs everytime a new scen...
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AddSoundToButtons();
    }

    private void AddSoundToButtons()
    {
         // Find all buttons in the scene
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            // Add actionListeners
            button.onClick.AddListener(() =>
            {
                if (SoundEffectManager.Instance != null)
                    SoundEffectManager.Instance.ButtonClick();
            });
        }
    }
}
