using UnityEngine;

public class Boss0 : monobehaviour
{
    private int state;
    private float timer;
    
    // Start is called once before the first execution of Update after the monobehaviour is created
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 3;
            int state = Random.Range(0,3);

            if (state==0)
        {
            Flower1Shooting a = GetComponent<Flower1Shooting>();
            if (a != null) {
                a.bulletsShoot = 3;
                a.fireDelay=0.7f;
            }
        }

        if (state==1)
        {
            Flower1Shooting a = GetComponent<Flower1Shooting>();
            if (a != null) {
                a.bulletsShoot = 1;
                a.fireDelay=0f;
            }
        }

        if (state==2)
        {
            Flower1Shooting a = GetComponent<Flower1Shooting>();
            if (a != null) {
                a.bulletsShoot = 24;
                a.fireDelay=1f;
            }
        }
        }
    }

    void FixedUpdate()
    {
        



}
}

