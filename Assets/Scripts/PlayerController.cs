using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerCtrl: MonoBehaviour
{
    public RoomControllerScript roomControllerScript;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveVector;

//
    public PlayerStats playerStats;
    
    public bool iFrame;
    private float IframeTimer;

    private float _t;
    private bool DashReady;
    private bool InDash=false;
    private float DashCD=5;

    private PlayerEffects playerEffects;
    [SerializeField] private Color overlay = Color.red;
//

    void Start ()
    {
        // fetch components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerEffects = GetComponent<PlayerEffects>();
    }

    void Update ()
    {
        if (PauseHandler.paused==true) {return;}

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

        if(iFrame) {
            IframeTimer +=Time.deltaTime;
            if(IframeTimer>playerStats.iFrameDuration)
            {
                iFrame=false;
                IframeTimer=0;
                playerEffects.ClearOverlay();
            }
        }
        if(!DashReady) {
            DashCD +=Time.deltaTime;
            if(DashCD>5)
            {
                DashReady=true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && DashReady)
        {
            InDash=true;
            _t=0;
            PlayerStats playerstats = GetComponent<PlayerStats>();
            playerstats.movementSpeed+=11;
            DashReady=false;
            DashCD=0;
        }

        if(_t>0.3 && InDash)
        {
            PlayerStats playerstats = GetComponent<PlayerStats>();
            playerstats.movementSpeed-=11;
            InDash=false;
        }//0.5 is the dash Duration
        _t+=Time.deltaTime;
        
    }

    void FixedUpdate ()
    {
        rb.linearVelocity = moveVector * playerStats.movementSpeed;
    }

    void OnTriggerStay2D(Collider2D other)
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
        if(iFrame) playerEffects.SetOverlay(overlay, 0.3f);
    }
    public bool GetiFrame()
    {
        return iFrame;
    }
}