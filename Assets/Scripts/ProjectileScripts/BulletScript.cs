using UnityEngine;

public class BulletScript : MonoBehaviour
{

private Vector3 mousePos;
private Camera mainCam;
private Rigidbody2D rb;
private float speed;

private float damage;

private int hp;

private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player=GameObject.FindGameObjectWithTag("player");
        hp = player.GetComponent<PlayerStats>().GetProjectileHits();

        
       if (player==null)
        {
            print("super important super error, nothing will work, unfixable and broken, never let this be printed!");
        }

        PlayerStats pc = player.GetComponentInChildren<PlayerStats>();
        
        if(pc!=null)
        {
            speed=pc.projectileSpeed;
            damage=pc.dmg;
            
        }
        

        mainCam=GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb=GetComponent<Rigidbody2D>();
        mousePos=mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos-player.transform.position;
        Vector3 rotation = player.transform.position-mousePos;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized*speed;
        float rot= Mathf.Atan2(rotation.y, rotation.x)*Mathf.Rad2Deg;
        transform.rotation=Quaternion.Euler(0,0,rot+180);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("enemy"))
        {
            other.GetComponent<LifeSystem>().TakeDamage(damage);
            hp-=1;
            if (hp<1) {
                Destroy(gameObject);
            }
        }
    }
}
