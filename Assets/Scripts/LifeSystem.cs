using UnityEngine;
using Unity.VisualScripting;

public class LifeSystem : MonoBehaviour
{   
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
        if(gameObject.TryGetComponent<PlayerCtrl>(out PlayerCtrl pc))
        {
            if (pc.GetiFrame())
            {
                Debug.Log("iFrames used.");
                return;
            }
            pc.SetiFrame(true);

        }
        
        if (TryGetComponent<AudioSource>(out AudioSource damageSound))
        {
            damageSound.Play();
        }

        currentHealth -= damage;
        //Debug.Log(gameObject.name + " took " + damage + " damage. Health: " + currentHealth);
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //-Debug.Log(gameObject.name + " died!");-
        if (!gameObject.CompareTag("player"))
        {
            Xp_handling xpHandeler = GameObject.FindWithTag("player")?.GetComponent<Xp_handling>();
            int toAddXp = gameObject.GetComponent<Enemy_Xp>().getXp();
            xpHandeler.addXp(toAddXp);
            //Debug.Log("Xp: " + xpHandeler.getPlayerXp());
            Destroy(gameObject);
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
