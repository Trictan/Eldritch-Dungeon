using UnityEngine;
using Unity.VisualScripting;

public class RangedWeapon : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    public GameObject bullet;
    public Transform BulletTransform;
    public bool canFire;
    private float timer;
    public float attackDelay;
    public float dmg;
    public float projectileSpeed;
    public Transform projectileFolder;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
        if(!canFire)
        {
            timer +=Time.deltaTime;
            animator.SetFloat("attackDelay", timer/attackDelay);
            if(timer>attackDelay)
            {
                canFire=true;
                animator.SetBool("canFire", canFire);
                timer=0;
            }
        }

        else if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            animator.SetBool("canFire", canFire);
            animator.SetTrigger("fire");
            //anim here
        
            Instantiate(bullet, BulletTransform.position, Quaternion.identity, projectileFolder);
        }

    }

}
