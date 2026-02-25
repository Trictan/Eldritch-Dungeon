using UnityEngine;

public class Enemy_Xp : MonoBehaviour
{
    public float Xp = 5;

    void Start()
    {
        int randomInt = Random.Range(0, 10);
        float bonus = Random.Range(0, Xp);

        if(randomInt == 9)
        {
            bonusXp(bonus);
        }
    }

    void bonusXp(float bonus)
    {
        Xp += bonus;
    }

    public float getXp()
    {
        return Xp;
    }
}
