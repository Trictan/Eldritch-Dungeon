using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private int level = 0; //Keep track of what level
    private float goalXp = 0;
    private int gainedLevels = 0;

    void Update()
    {
        checkLevelUp();
    }
    void newLevel()
    {   
        if(level == 0) goalXp = 20;
        else goalXp *= 4; //If want make a algoritm.
        level ++;
    }

    public int getLevel()
    {
        return level;
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
