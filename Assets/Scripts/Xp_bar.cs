using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Xp_bar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text LevelText;

    public GameObject player;

    public Image levelUpSymbol;
    private int maxXpLevel = 0;
    private int minXpLevel = 0;
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

        slider.minValue = minXpLevel;
        slider.maxValue = maxXpLevel;

        level = player.GetComponent<LevelSystem>().getLevel();
        LevelText.text = "Level " + level;

        SoundEffectManager.Instance.LevelUp();
        symbolVisability();

        //Debug.Log("Updated Xp bar");
    }

    public void symbolVisability()
    {
        bool levelUp = false;
        if(player.GetComponent<LevelSystem>().getGainedLevels() > 0){
            levelUp = true;
        }
        levelUpSymbol.gameObject.SetActive(levelUp);
    }
}
