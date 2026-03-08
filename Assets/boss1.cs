using System.Runtime.InteropServices;
using UnityEngine;

public class boss1 : MonoBehaviour
{
    private int state;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Flower1Shooting shootingScript;
    void Start()
    {
        shootingScript=GetComponent<Flower1Shooting>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 4;
            int state = Random.Range(0,4);

            if (state==0)
            {
            AIChase chase = GetComponent<AIChase>();
            chase.speed=0;
            shootingScript.enabled=true;
            Invoke("CancelAttack", 4f);
            }
        

            if (state==1)
            {
                AIChase chase = GetComponent<AIChase>();
                chase.speed=0;
                Invoke("EvilDashOfDoom", 2f);
                Invoke("notEvilDashOfDoom", 2.1f);
            }

            if (state==2)
            {
                AIChase chase = GetComponent<AIChase>();
                chase.speed=4;
            }
            if (state==3)
            {
                shootingScript.enabled=true;
                shootingScript.specialAttack=true;
                shootingScript.bulletsShoot=24;
                shootingScript.fireDelay=1;
                Invoke("EndState3", 4);
            }
        }
    }
    private void EvilDashOfDoom()
    {
        AIChase chase = GetComponent<AIChase>();
        chase.speed=20;
    }
    private void notEvilDashOfDoom()
    {
        AIChase chase = GetComponent<AIChase>();
        chase.speed=2;
    }
    private void CancelAttack()
    {
        AIChase chase = GetComponent<AIChase>();
        shootingScript.enabled=false;
    }
    private void EndState3()
    {
        shootingScript.enabled=false;
        shootingScript.specialAttack=false;
        shootingScript.bulletsShoot=1;
        shootingScript.fireDelay=4;
    }

}
