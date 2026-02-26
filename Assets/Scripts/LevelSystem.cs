using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private int level = 0; //Keep track of what level
    private int goalXp = 0;
    private int gainedLevels = 0;
    
    void Start()
    {
        //set base values for level 1;
        level = 1;
        goalXp = 20; //Depending on how hard mabey??
    }

    void Update()
    {
        checkLevelUp();
    }
    void newLevel()
    {   
        level ++;
        goalXp *= 4; //If want make a algoritm.
    }

    public int getLevel()
    {
        return level;
    }

    public int getGoalXp()
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
            GameObject.FindWithTag("XpBar")?.GetComponent<Xp_bar>().newLevel();
            gainedLevels ++;
        }
    }

    public int getGainedLevels()
    {
        return gainedLevels;
    }

    public void resetGainedLevels()
    {
        gainedLevels = 0;
    }
}
