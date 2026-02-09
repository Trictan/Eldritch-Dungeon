using System;
using UnityEngine;

public class PlayerCtrl :MonoBehaviour
{
    public RoomControllerScript roomControllerScript;
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveVector;

//
    public PlayerStats reference;
    
    public bool iFrame;
    private float timer;
//

    void Start ()
    {
        // fetch components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update ()
    {
        // get movement input
        float speedX = Input.GetAxisRaw("Horizontal");
        float speedY = Input.GetAxisRaw("Vertical");
        // create and normalize direction vector
        moveVector = new Vector2(speedX, speedY).normalized;

        // update animation direction if direction is non-zero
        if (moveVector != Vector2.zero) {
            animator.SetFloat("speedX", speedX);
            animator.SetFloat("speedY", speedY);
            animator.SetBool("isWalking", true);
        } else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void FixedUpdate ()
    {
        rb.linearVelocity = moveVector * moveSpeed;
            if(iFrame)
        {
            timer +=Time.deltaTime;
            if(timer>reference.iFrames)
            {
                iFrame=false;
                timer=0;
            }
        }
      
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "door")
        {
            GameObject door = other.gameObject;
            roomControllerScript.setRoom(door, this.gameObject);
        }
    }
        public void SetiFrame(bool val)
    {
        iFrame=val;
    }
    public bool GetiFrame()
    {
        return iFrame;
    }
}