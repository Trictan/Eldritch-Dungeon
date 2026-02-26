using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Xp_bar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text LevelText;

    public GameObject player;
    private float maxXpLevel = 0;
    private float minXpLevel = 0;
    private int level = 0;
    
    void Start()
    {
        slider.value = 0;
        newLevel();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.GetComponent<Xp_handling>().getPlayerXp();
    }

    //Should be called after/during upgrade
    public void newLevel()
    {
        minXpLevel = maxXpLevel; //Last max is the new min
        maxXpLevel = player.GetComponent<LevelSystem>().getGoalXp();

        slider.maxValue = maxXpLevel;

        level = player.GetComponent<LevelSystem>().getLevel();
        LevelText.text = "Level " + level;
    }
}
