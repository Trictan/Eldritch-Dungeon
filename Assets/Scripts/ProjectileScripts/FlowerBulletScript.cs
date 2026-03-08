using UnityEngine;

public class FlowerBulletScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float speed;
    public float damage;
    private Vector2 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();      
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        rb.linearVelocity = direction * speed;

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
