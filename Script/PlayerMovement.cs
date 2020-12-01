using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private float direction;
    private bool facingRight = false, isGrounded;
    private Rigidbody2D rb;
    private Animator animator;
    private Flight flight;
    public LayerMask layer;
    public float distance;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        flight = GetComponent<Flight>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector3.down, distance,layer);
        direction = Input.GetAxisRaw("Horizontal");
        Flip();
        Animation();
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        JumpPhysics();
    }

    public void JumpPhysics()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    public void Flip()
    {
        if (!facingRight && direction > 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, transform.rotation.w);
           
            facingRight = true;
        }
        else if(facingRight && direction < 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, transform.rotation.w);
            facingRight = false;
        }
    }

    public void Animation()
    {
        if (!flight.FlightStatus())
        {
            animator.SetBool("isFlying", false);
            
            if (direction != 0 && isGrounded)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            animator.SetBool("isFlying", true);
            animator.SetBool("isWalking", false);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * distance));
    }
}
