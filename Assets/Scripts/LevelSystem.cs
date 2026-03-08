using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private PlayerStats playerStats; //Keep track of what level
    private float goalXp = 0;
    private int gainedLevels = 0;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        checkLevelUp();
    }
    void newLevel()
    {   
        if(playerStats.lvl == 0) goalXp = 20;
        else goalXp *= 4; //If want make a algoritm.
        playerStats.lvl += 1;
    }

    public int getLevel()
    {
        return playerStats.lvl;
    }

    public float getGoalXp()
    {
        return goalXp;
    }

    public void checkLevelUp()
    {
        int currentXp = GetComponent<Xp_handling>().getPlayerXp();
        //Debug.Log("Xp level up check: current = " + currentXp + " goal = " + goalXp);
        if(currentXp >= goalXp)
        {
            newLevel();
            gainedLevels ++;
            GameObject.FindWithTag("XpBar")?.GetComponent<Xp_bar>().newLevel();
        }
    }

    public int getGainedLevels()
    {
        return gainedLevels;
    }

    public void resetGainedLevels()
    {
        gainedLevels = 0;
        GameObject.FindWithTag("XpBar")?.GetComponent<Xp_bar>().symbolVisability();
    }
}
