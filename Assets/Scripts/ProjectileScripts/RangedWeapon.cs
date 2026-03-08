using UnityEngine;
using Unity.VisualScripting;

public class RangedWeapon : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    public GameObject projectile;
    public Transform BulletTransform;
    private float timer;

    private Transform projectileFolder;
    private Animator animator;

    private PlayerStats playerStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
                
            }
            else if (playerStats.numberOfProjectiles == 2)
            {
                Quaternion spawnRotation = BulletTransform.rotation * Quaternion.Euler(0, 45, 0);
                SpawnProjectile(RotateVector(mousePos, 90));
                //Instantiate(bullet, BulletTransform.position, Quaternion.identity, projectileFolder);
            }
            
        }

    void SpawnProjectile(Vector2 dir)
    {
        GameObject bullet = Instantiate(projectile, BulletTransform.position, Quaternion.identity, projectileFolder);
        bullet.GetComponent<BulletScript>().SetDirection(dir);
    }
    }

Vector2 RotateVector(Vector2 basedir, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        return new Vector2(basedir.x * cos - basedir.y * sin, basedir.x * sin + basedir.y * cos);
    }

}
