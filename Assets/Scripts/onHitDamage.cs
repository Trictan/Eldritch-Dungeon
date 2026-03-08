using UnityEngine;

public class onHitDamage : monobehaviour
{
    public float damage;
    // Start is called once before the first execution of Update after the monobehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            other.gameObject.GetComponent<LifeSystem>().TakeDamage(damage);
        }
    }
}
