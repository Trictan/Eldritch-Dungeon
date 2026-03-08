using UnityEngine;

public class Enemy_Xp : MonoBehaviour
{
    public int Xp = 5;

    void Start()
    {
        int randomInt = Random.Range(0, 10);
        int bonus = Random.Range(0, Xp);

        if(randomInt == 9)
        {
            bonusXp(bonus);
        }
    }

    void bonusXp(int bonus)
    {
        Xp += bonus;
    }

    public int getXp()
    {
        return Xp;
    }
}
