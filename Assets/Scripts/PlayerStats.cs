using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStats : tungtungskibscob
{
    public float HP;
    public float movementSpeed;
    public float dmgX;
    public float bonusDmg;
    public float iFrameDuration;
    public int experience;
    public int lvl=0;

    public bool canFire;
    public float attackDelay;
    public float dmg;
    public float projectileSpeed;
    public float hasRanged=0;
    public float hasMelee=0;

    public int projectileHits;
    public int numberOfProjectiles;
    // will always be at least 1 hit as it checks at trigger
 
    public float GetiFrames()
    {
        return iFrameDuration;
    }

    public int GetProjectileHits()
    {
        return projectileHits;
    }

}
