using UnityEngine;

public class FlowerBulletScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float speed;
    public float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("player");
        
        Vector3 direction = player.transform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
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
        if (other.CompareTag("player"))
        {
            other.GetComponent<LifeSystem>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
