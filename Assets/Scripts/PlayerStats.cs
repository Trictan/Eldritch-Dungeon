using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float HP;
    public float movementSpeed;
    public float dmgX;
    public float bonusDmg;
    public float iFrameDuration;
    public int experience;
    public int lvl;
 
    public float GetiFrames()
    {
        return iFrameDuration;
    }

}
