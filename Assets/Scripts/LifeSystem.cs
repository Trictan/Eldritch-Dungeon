using UnityEngine;
using Unity.VisualScripting;

public class LifeSystem : MonoBehaviour
{   
    public float startHealth;
    float currentHealth;
    private PlayerStats playerStats = null;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    currentHealth=startHealth;
    if(gameObject.CompareTag("player")) playerStats = GetComponent<PlayerStats>();
    }

    void FixedUpdate()
    {
        if(playerStats != null)
        {
            if(playerStats.HP > currentHealth)
            {
                currentHealth = Mathf.Clamp(playerStats.HP, 0f, 6f);
            }
        }
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

        currentHealth -= damage;
        //Debug.Log(gameObject.name + " took " + damage + " damage. Health: " + currentHealth);

        if(playerStats != null)
        {
            //Keep the player stats up to date.
            playerStats.HP = currentHealth;
        }
        
        if(currentHealth <= 0)
        {
            Die();
            return;
        }

        // crashes otherwise
        if (SoundEffectManager.Instance==null) {return;}

        if (gameObject.CompareTag("enemy"))
        {
            SoundEffectManager.Instance.EnemyTakeDamage(transform.position);
        }
        if (gameObject.CompareTag("player"))
        {
            SoundEffectManager.Instance.PlayerTakeDamage(transform.position);
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

            if (SoundEffectManager.Instance==null) {return;}
            SoundEffectManager.Instance.EnemyDie(transform.position);
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public float GetMaxHealth()
    {
        return startHealth;
    }
}
