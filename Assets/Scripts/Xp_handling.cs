using UnityEngine;

public class Xp_handling : MonoBehaviour
{
    private int Xp = 0;

    public void addXp(int add)
    {
        Xp += add;
    }
    public int getPlayerXp()
    {
        return Xp;
    }
}
