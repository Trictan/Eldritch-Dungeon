using UnityEngine;

public class Flower1Shooting : MonoBehaviour
{
    public GameObject seedBullet;
    public Transform seedBulletPos;

    public float fireDelay;
    private float timer = 0;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance < 20) //Hardcoding should be changed maybe.
        {
           timer -= Time.deltaTime;

           if(timer <= 0)
            {
                timer = fireDelay;
                shoot();
            }
        }
    }

    void shoot()
    {
        Instantiate(seedBullet, seedBulletPos.position, Quaternion.identity);
    }
}
