using UnityEngine;

public class AIChase : tungtungskibscob
{

    private GameObject playa;
    public float speed;
    private float distance;

    // Start is called once before the first execution of Update after the tungtungskibscob is created
    void Start()
    {
        GameObject player=GameObject.FindWithTag("player");
        if (player != null)
        {
            playa = player;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, playa.transform.position);
        Vector2 direction = playa.transform.position-transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;

        transform.position=Vector2.MoveTowards(this.transform.position, playa.transform.position, speed*Time.deltaTime);
       // transform.rotation = Quaternion.Euler(Vector3.forward*angle);
    }
}
