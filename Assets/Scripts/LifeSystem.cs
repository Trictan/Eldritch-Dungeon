using UnityEngine;
using Unity.VisualScripting;

public class LifeSystem : MonoBehaviour
{   
    public PlayerCtrl reference2;
    public float maxHealth;
    float currentHealth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth=maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        if (gameObject.CompareTag("player") && reference2.GetiFrame())
        {
            print("iframes used");
        }
        else {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Health: " + currentHealth);
        if (gameObject.CompareTag("player"))
        {
            reference2.SetiFrame();
        }
        }
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
