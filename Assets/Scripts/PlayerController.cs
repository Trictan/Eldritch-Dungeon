using UnityEngine;

public class PlayerCtrl :MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveVector;


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
    }
}