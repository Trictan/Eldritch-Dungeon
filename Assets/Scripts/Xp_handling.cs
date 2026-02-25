using UnityEngine;

public class Xp_handling : MonoBehaviour
{
    private float Xp = 0;

    public void addXp(float add)
    {
        Xp += add;
    }
    public float getPlayerXp()
    {
        return Xp;
    }
}
