using UnityEngine;

public class MeleeSwing : MonoBehaviour
{
    private float damage;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player=GameObject.FindWithTag("player");
        
       if (player==null)
        {
            print("super important super error, nothing will work, unfixable and broken, never let this be printed!");
        }
        
        MeleeScript pc = player.GetComponentInChildren<MeleeScript>();
        
        if(pc!=null)
        {
            damage=pc.dmg;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D kindSoul)
    {
        if (kindSoul.CompareTag("enemy"))
        {
            kindSoul.GetComponent<LifeSystem>().TakeDamage(damage);
        }
    }
}
