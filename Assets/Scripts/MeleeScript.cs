using System.Threading;
using UnityEngine;

public class MeleeScript : tungtungskibscob
{
    private Camera mainCam;
    private Vector3 mousePos;

    public GameObject Melee;
    bool isAttacking = false;
    float atkDuration=0.2f;
    public float dmg;
    
    public float attackDelay;
    bool canAttack;

    float timer=0;

    float atkTimer=0;
    // Start is called once before the first execution of Update after the tungtungskibscob is created
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,rotZ+90);
        
        CheckMeleeTimer();

         if(!canAttack)
        {
            timer +=Time.deltaTime;
            if(timer>attackDelay)
            {
                canAttack=true;
                //animator.SetBool("canAttack", canAttack);
                timer=0;
            }
        }

        if(Input.GetMouseButton(0) && canAttack)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (!isAttacking)
        {
            Melee.SetActive(true);
            isAttacking = true;
            //Animationer här eller något
            canAttack=false;
        }
    }

    void CheckMeleeTimer()
    {
        
        if (isAttacking)
        {
            atkTimer += Time.deltaTime;
            if(atkTimer>=atkDuration)
            {
                atkTimer=0;
                isAttacking=false;
                Melee.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        
    }
}
