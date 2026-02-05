using UnityEngine;
using Unity.VisualScripting;

public class LifeSystem : MonoBehaviour
{
    private float currentHealth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float maxHealth = (float) Variables.Object(this.gameObject).Get("maxHealth");
        float currentHealth = (float) Variables.Object(this.gameObject).Get("Health");
        print(currentHealth);
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {

        print(damage);
        currentHealth=currentHealth - damage;
        print(currentHealth);
        Variables.Object(this.gameObject).Set("currentHealth",currentHealth);
        print(currentHealth);
        Debug.Log(gameObject.name + " took " + damage + " damage. Health: " + currentHealth);

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
