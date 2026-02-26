using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private int level = 0; //Keep track of what level
    private float goalXp = 0;
    
    void Start()
    {
        //set base values for level 1;
        level = 1;
        goalXp = 20; //Depending on how hard mabey??
    }
    void newLevel()
    {
        level ++;
        goalXp *= 2; //If want make a algoritm.
    }

    public int getLevel()
    {
        return level;
    }

    public float getGoalXp()
    {
        return goalXp;
    }

    public bool checkLevelUp()
    {
        float currentXp = GetComponent<Xp_handling>().getPlayerXp();
        if(currentXp >= goalXp)
        {
            newLevel();
            return true;
        }
        return false;
    }
}
