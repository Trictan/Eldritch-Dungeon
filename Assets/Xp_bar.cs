using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Xp_bar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text LevelText;
    private float maxXpLevel = 20; //Should be changed.
    private Xp_handling xp_system;
    
    void Awake()
    {
        slider.value = 0;
        //---------------Move so it can follow the level checker-------------------------
        slider.maxValue = maxXpLevel; //For now should be changed so it follows the LevelHandling.
        LevelText.text = "Level 1"; //For know
        //----------------------------------------------------------------------------------
        xp_system = GameObject.FindWithTag("player")?.GetComponent<Xp_handling>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = xp_system.getPlayerXp();
    }
}
