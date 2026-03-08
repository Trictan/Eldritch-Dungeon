using UnityEngine;
using Unity.VisualScripting;

public class RangedWeapon : tungtungskibscob
{
    private Camera mainCam;
    private Vector3 mousePos;

    public GameObject projectile;
    public Transform BulletTransform;
    private float timer;

    private Transform projectileFolder;
    private Animator animator;

    private PlayerStats playerStats;

    // Start is called once before the first execution of Update after the tungtungskibscob is created
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerStats>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        projectileFolder = GameObject.FindGameObjectWithTag("projectileParent").transform;
        animator = BulletTransform.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseHandler.paused==true) {return;}

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,rotZ);
        Quaternion fiveDegree = Quaternion.Euler(45,45,45);
        if(!playerStats.canFire)
        {
            timer +=Time.deltaTime;
            animator.SetFloat("attackDelay", timer/playerStats.attackDelay);
            if(timer>playerStats.attackDelay)
            {
                playerStats.canFire=true;
                animator.SetBool("canFire", playerStats.canFire);
                timer=0;
            }
        }

        else if (Input.GetMouseButton(0) && playerStats.canFire)
        {
            playerStats.canFire = false;
            animator.SetBool("canFire", playerStats.canFire);
            animator.SetTrigger("fire");
            //anim here
            if (playerStats.numberOfProjectiles == 1)
            {
                SpawnProjectile(0);
            }
            else if (playerStats.numberOfProjectiles == 2)
            {
                SpawnProjectile(-5);
                SpawnProjectile(5);
            }
            else if (playerStats.numberOfProjectiles == 3)
            {
                SpawnProjectile(-7);
                SpawnProjectile(0);
                SpawnProjectile(7);
            }
            else
            {
                SpawnProjectile(-90);
                SpawnProjectile(0);
                SpawnProjectile(90);
                SpawnProjectile(180);
            }
            }
            
        }

    void SpawnProjectile(float spread)
    {
        GameObject bullet = Instantiate(projectile, BulletTransform.position, Quaternion.identity, projectileFolder);
        BulletScript tempbullet = bullet.GetComponent<BulletScript>();
        if (tempbullet != null)
            {
                tempbullet.bulletSpread=spread;
            }
    }
}